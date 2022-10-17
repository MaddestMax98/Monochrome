using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class SceneManager : MonoBehaviour
    {
        void OnEnable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //Debug.Log("OnSceneLoaded: " + scene.name);
            //Debug.Log(mode);
            CameraManager.CamerasSetup();
        }

        void OnDisable()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}

