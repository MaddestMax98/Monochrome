using Cinemachine;
using ScriptedCamera;
using UnityEngine;

namespace PlayerCharacter 
{
    public class Phone : MonoBehaviour
    {
        [SerializeField]
        private GameObject phone;
        [SerializeField]
        private CinemachineVirtualCamera fpsCamera;
        private bool isturnedOn = false;
        // Start is called before the first frame update
        private void OnEnable()
        {
            phone.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && !isturnedOn)
            {
                TurnOnPhone();
            }
            else if (Input.GetKeyDown(KeyCode.E) && isturnedOn)
            {
                TurnOffPhone();
            }
        }

        private void TurnOnPhone()
        {
            isturnedOn = true;
            phone.SetActive(true);
            Manager.CameraManager.PreviousCamera = Manager.CameraManager.ActiveCamera;
            CameraSwitcher.SwitchCamera(fpsCamera);
        }

        private void TurnOffPhone()
        {
            isturnedOn = false;
            phone.SetActive(false);
            CameraSwitcher.SwitchCamera(Manager.CameraManager.PreviousCamera);
        }
    }
}