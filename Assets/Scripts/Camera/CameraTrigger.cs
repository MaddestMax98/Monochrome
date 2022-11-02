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
        private CinemachineVirtualCamera camera1;
        [SerializeField]
        private CinemachineVirtualCamera camera2;
        [SerializeField]
        [Tooltip("Set camera1 as active camera")]
        private bool isLevelStart = false;

        private void Start()
        {
            if (isLevelStart)
            {
                CameraSwitcher.SwitchCamera(camera1);
                CameraManager.PreviousCamera = camera1;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (CameraManager.ActiveCamera == camera1)
                {
                    CameraSwitcher.SwitchCamera(camera2);
                }
                else
                {
                    CameraSwitcher.SwitchCamera(camera1);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //Debug.Log("Ah merde");
        }
    }
}

