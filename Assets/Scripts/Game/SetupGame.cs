using ScripatbleObj;
using UnityEngine;

/// <summary>
/// Setup all the scriptable objects of the game properly when starting a new game.
/// TODO: Later on load from save file data to set up..
/// </summary>
public class SetupGame : MonoBehaviour
{
    [SerializeField] PlayerInventoryData playerInventoryData;
    [SerializeField] MannequinInventoryData mannequinInventoryData;
    [SerializeField] BrokenItemData brokenHeater;
    [SerializeField] BrokenItemData brokenAlarm;
    [SerializeField] MannequinItemData mannequinHat;
    [SerializeField] UsableItemData wrench;
    [SerializeField] UsableItemData hammer;

    public void SetupDemo()
    {
        playerInventoryData.ResetInventory();
        mannequinInventoryData.ResetInventory();

        brokenHeater.isMainTask = false;
        brokenHeater.state = BrokenItemState.Cascade;

        brokenAlarm.isMainTask = true;
        brokenHeater.state = BrokenItemState.NotImportant;

        mannequinHat.isPickedUp = false;
        mannequinHat.isEquiped = false;

        wrench.isPickedUp = false;
        hammer.isPickedUp = false;


        PlayerPrefs.SetInt("HAS_INTERACTED_TASKMACHINE", 0);
        PlayerPrefs.SetString("CURRENT_SPAWN_POINT", "SPAWN_SaveRoom");
    }
}
