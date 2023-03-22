using AnomalySystem.ScriptableObjects;
using ScripatbleObj;
using ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Setup all the scriptable objects of the game properly when starting a new game.
/// TODO: Later on load from save file data to set up..
/// </summary>
public class SetupGame : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] PlayerInventoryData playerInventoryData;
    [SerializeField] MannequinInventoryData mannequinInventoryData;
    [SerializeField] BrokenItemData brokenHeater;
    [SerializeField] BrokenItemData brokenAlarm;
    [SerializeField] MannequinItemData mannequinHat;
    [SerializeField] StoryItem deptPaper;

    [SerializeField] MessageStorageData messageStorage;

    [SerializeField] List<UsableItemData> usableItems;

    [SerializeField] List<CleanItemData> cleanItemDatas;

    [SerializeField] List<AnomalyHandlerData> anomalyHandlerDatas;

    public void SetupDemo()
    {
        playerData.sanity = 5;

        messageStorage.isScenePersistenceLinked = false;
        messageStorage.isWaitingForResponse = false;
        messageStorage.totalWifeRespones = 0;
        messageStorage.totalWorkRespones = 0;
        messageStorage.totalPsychRespones = 0;

        playerInventoryData.ResetInventory();
        mannequinInventoryData.ResetInventory();

        brokenHeater.isMainTask = false;
        brokenHeater.state = BrokenItemState.CurrentTask;

        brokenAlarm.isMainTask = true;
        brokenAlarm.state = BrokenItemState.NotImportant;

        mannequinHat.isPickedUp = false;
        mannequinHat.isEquiped = false;

        deptPaper.isMainTask = true; //?
        deptPaper.state = StoryItemState.Hidden; //?

        for (int i = 0; i < usableItems.Count; i++)
        {
            usableItems[i].isPickedUp = false;
        }

        for (int i = 0; i < cleanItemDatas.Count; i++)
        {
            cleanItemDatas[i].isPickedUp = false;
            cleanItemDatas[i].isUsed = false;
        }

        for (int i = 0; i < anomalyHandlerDatas.Count; i++)
        {
            anomalyHandlerDatas[i].currentAnomalies = 0;
        }

        PlayerPrefs.SetInt("HAS_INTERACTED_TASKMACHINE", 0);
        PlayerPrefs.SetString("CURRENT_SPAWN_POINT", "SPAWN_SaveRoom");
    }
}
