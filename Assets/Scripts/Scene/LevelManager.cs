using Cinemachine;
using ScripatbleObj;
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
                //spawnName = "SPAWN_EntranceBuilding";
                //For the demo
                spawnName = "SPAWN_SaveRoom";
            }
            else
            {
                spawnName = PlayerPrefs.GetString("CURRENT_SPAWN_POINT");
                //for debug uncomment this and use specific spawn point
                //spawnName = "SPAWN_SaveRoom";
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            //Debug.Log("OnSceneLoaded: " + scene.name);
            //Debug.Log(mode);
            /*-----------------Setup BrokenItems-----------------*/
            /* Set the item either to not important if its not part of the current day task or if its already been repaired the previous day.
             * Or set the item to cascade if its not been repaired the previous day
             * The rest is handled in the brokenitem itself 
             */
            BrokenItemData[] previousBrokenItems = GetComponent<DaySystem>().GetPreviousDayBrokenItems();
            BrokenItemData[] currentBrokenItems = GetComponent<DaySystem>().GetCurrentDayBrokenItems();
            BrokenItem[] itemsInScene = GameObject.FindObjectsOfType<BrokenItem>(); //Find all items in the scene

            if (previousBrokenItems != null) //This is for the first day
            {
                for (int i = 0; i < previousBrokenItems.Length; i++)
                {
                    foreach (BrokenItem sceneItem in itemsInScene) //UnOptimal but we have no choice
                    {
                        if (sceneItem.Data == previousBrokenItems[i])
                        {
                            //Debug.Log(sceneItem.gameObject.name);
                            switch (sceneItem.Data.state)
                            {
                                case BrokenItemState.CurrentTask:
                                    sceneItem.Data.state = BrokenItemState.Cascade;
                                    break;
                                case BrokenItemState.IsRepaired: //Do we really need this step?
                                    sceneItem.Data.state = BrokenItemState.NotImportant;
                                    break;
                            }
                        }
                    }

                }
            }


            for (int i = 0; i < currentBrokenItems.Length; i++)
            {
                foreach (BrokenItem sceneItem in itemsInScene)
                {
                    if (sceneItem.Data == currentBrokenItems[i])
                    {
                        switch (sceneItem.Data.state)
                        {
                            case BrokenItemState.NotImportant:
                                sceneItem.Data.state = BrokenItemState.CurrentTask;
                                break;
                        }
                        
                    }
                }

            }

            /*-----------------Setup Player-----------------*/
            int index = GetSpawnIndex(spawnName);
            Instantiate(player, spawnPoints[index].transform.localPosition, spawnPoints[index].transform.localRotation);
            /*-----------------Setup Cameras-----------------*/
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

