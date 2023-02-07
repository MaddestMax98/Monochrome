using PlayerCharacter;
using ScripatbleObj;
using UnityEngine;

public class TaskMachine : Interactable
{
    [SerializeField]
    StartDay saveRoomDoor;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("HAS_INTERACTED_TASKMACHINE") != 0)
        {
            saveRoomDoor.EnableSaveRoomDoor();
        }
    }

    public override void Interact()
    {
        PlayerPrefs.SetInt("HAS_INTERACTED_TASKMACHINE", 1);
        saveRoomDoor.EnableSaveRoomDoor();
    }
}
