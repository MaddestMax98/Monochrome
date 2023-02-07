using PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Disable the collision so that the player has to first get his task from the task machine
public class StartDay : MonoBehaviour
{
    private void Start()
    {
        if(PlayerPrefs.GetInt("HAS_INTERACTED_TASKMACHINE") != 1)
        {
            GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = true;
        }
    }

    public void EnableSaveRoomDoor()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
}
