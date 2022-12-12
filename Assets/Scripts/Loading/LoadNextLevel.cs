using PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextLevel : MonoBehaviour
{
    [SerializeField]
    private LoadingScene.Scene scene;
    [SerializeField]
    private string spawnName;

    [SerializeField]
    private bool isTrigger = false;
    public void LoadNextScene()
    {
        PlayerPrefs.SetString("CURRENT_SPAWN_POINT", spawnName);
        LoadingScene.Load(scene);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            if (isTrigger)
            {
                LoadNextScene();
            }
        }

    }
}
