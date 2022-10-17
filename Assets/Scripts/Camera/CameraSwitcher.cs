using System.Collections.Generic;
using System.Diagnostics;
using Cinemachine;
using Manager;

namespace ScriptedCamera
{
    //TODO: Optimize this code so it switches from the current static camera
    //      to the fps camera to avoid looping through all the cameras in the scene
    public static class CameraSwitcher
    {
        //Changes the priority of the cameras
        public static void SwitchCamera(CinemachineVirtualCamera camera)
        {
            camera.Priority = 666;
            CameraManager.ActiveCamera = camera;
            foreach (CinemachineVirtualCamera cam in CameraManager.Cameras)
            {
                if (cam != camera && cam.Priority != 0)
                {
                    cam.Priority = 0;
                }
            }
        }
    }

}
