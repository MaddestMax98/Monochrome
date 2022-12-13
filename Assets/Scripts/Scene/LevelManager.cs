using Cinemachine;
using ScriptedCamera;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject player;
        [SerializeField]
        private List<GameObject> spawnPoints = new List<GameObject>();
        [SerializeField]
        private bool isStartGame = false;
        private string spawnName;

        void OnEnable()
        {
            //TODO: Set isStartGame to true after they started the game in the save file
            if (isStartGame)
            {
                spawnName = "SPAWN_EntranceBuilding";
            }
            else
            {
                //spawnName = PlayerPrefs.GetString("CURRENT_SPAWN_POINT");
                //for debug uncomment this and use specific spawn point
                spawnName = "SPAWN_SaveRoom";
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //Debug.Log("OnSceneLoaded: " + scene.name);
            //Debug.Log(mode);

            int index = GetSpawnIndex(spawnName);
            Instantiate(player, spawnPoints[index].transform.localPosition, spawnPoints[index].transform.localRotation);

            CameraManager.CamerasSetup();

            CameraSwitcher.SwitchCamera(spawnPoints[index].GetComponent<CameraReference>().Camera);
            CameraManager.PreviousCamera = spawnPoints[index].GetComponent<CameraReference>().Camera;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private int GetSpawnIndex(string spawn)
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                if (spawnPoints[i].name == spawn)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}

