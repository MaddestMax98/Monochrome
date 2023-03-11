using PlayerCharacter;
using ScripatbleObj;
using UnityEngine;

public class TaskMachine : Interactable
{
    [SerializeField]
    StartDay saveRoomDoor;

    private AudioSource _sfx;

    private void Awake()
    {
        _sfx = GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("HAS_INTERACTED_TASKMACHINE") != 0)
        {
            saveRoomDoor.EnableSaveRoomDoor();
        }
    }

    public override void Interact()
    {
        PlayerPrefs.SetInt("HAS_INTERACTED_TASKMACHINE", 1);
        saveRoomDoor.EnableSaveRoomDoor();
        _sfx.Play();
    }
}
