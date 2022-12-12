using Cinemachine;
using Manager;
using UnityEngine;

namespace ScriptedCamera
{
    /// <summary>
    /// Switch cameras on trigger collision
    /// </summary>
    public class CameraTrigger : MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;
        [SerializeField]
        private bool playerThirdPersonCamera = false;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                ChangeCamera(other);
            }
        }

        private void ChangeCamera(Collider other = null)
        {
            if (playerThirdPersonCamera)
            {
                CameraSwitcher.SwitchCamera(other.transform.Find("TPS-Camera").GetComponent<CinemachineVirtualCamera>());
            }
            else
            {
                CameraSwitcher.SwitchCamera(virtualCamera);
            }
        }
    }
}

