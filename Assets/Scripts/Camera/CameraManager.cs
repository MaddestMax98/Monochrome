using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

namespace Manager
{
    public static class CameraManager
    {
        private static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
        private static CinemachineVirtualCamera activeCamera = null;
        //This variable references the camera that the player will transition into after switching from FPS view.
        private static CinemachineVirtualCamera previousCamera = null;

        public static List<CinemachineVirtualCamera> Cameras { get => cameras; set => cameras = value; }
        public static CinemachineVirtualCamera ActiveCamera { get => activeCamera; set => activeCamera = value; }
        public static CinemachineVirtualCamera PreviousCamera { get => previousCamera; set => previousCamera = value; }

        //TODO: Test speed of function to see if it needs optimization. 
        /// <summary>
        /// Clear the virtual camera list and add in all new virtual cameras found in the current scene
        /// </summary>
        public static void CamerasSetup()
        {
            cameras.Clear();

            CinemachineVirtualCamera[] newCameras = Object.FindObjectsOfType<CinemachineVirtualCamera>();
            for (int i = 0; i < newCameras.Count(); i++)
            {
                cameras.Add(newCameras[i]);
            }
        }
    }
}

