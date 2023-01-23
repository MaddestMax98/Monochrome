using TMPro.EditorUtilities;
using UnityEngine;

public class IKFootPlacement : MonoBehaviour
{
    // Using video: https://www.youtube.com/watch?v=rGB1ipH6DrM

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    [Tooltip("Select all layers that you want the raycast to see (Layers not included here will be ignored).")]
    private LayerMask _layerMask;

    [SerializeField]
    [Range(0, 1)]
    private float _floorDistance;

    private void OnAnimatorIK(int layerIndex)
    {
        if (_animator)
        {
            //Setting how much influence the Inverse Kinematics affect feet (Closer to 1 the more it does and 0 to less)
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, _animator.GetFloat("IKLeftFootWeight"));
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, _animator.GetFloat("IKLeftFootWeight"));
            
            _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, _animator.GetFloat("IKRightFootWeight"));
            _animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, _animator.GetFloat("IKRightFootWeight"));

            //Left Foot
            GetFootPosition(AvatarIKGoal.LeftFoot);
            //Right Foot
            GetFootPosition(AvatarIKGoal.RightFoot);
        }
    }

    private void GetFootPosition(AvatarIKGoal targetedFoot)
    {
        RaycastHit hit;
        Ray newRay = new Ray(_animator.GetIKPosition(targetedFoot) + Vector3.up, Vector3.down);

        if (Physics.Raycast(newRay, out hit, _floorDistance + 2f, _layerMask))
        {
            if (hit.transform.tag == "Walkable")
            {
                Vector3 footPosition = hit.point;
                footPosition.y += _floorDistance;
                _animator.SetIKPosition(targetedFoot, footPosition);
                Vector3 forward = Vector3.ProjectOnPlane(transform.forward, hit.normal);
                _animator.SetIKRotation(targetedFoot, Quaternion.LookRotation(forward, hit.normal));
                //_animator.SetIKRotation(targetedFoot, Quaternion.LookRotation(transform.forward, hit.normal));
            }
        }
    }
}
