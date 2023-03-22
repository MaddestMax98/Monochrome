using UnityEngine;
using ScripatbleObj;
using UnityEngine.UI;
using PlayerCharacter;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PhoneUI : MonoBehaviour
{
    [SerializeField]
    private PlayerInventoryData inventoryData;
    private int currentItemIndex = 0;

    //Index 0 = Wife, 1 = work, 2 = psychologist
    private bool[] HighlightStatus = new bool[3] { false, false, false };
    private bool hasNewMessages = false;

    [SerializeField] private Sprite[] signalState;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject Contacts;
    [SerializeField] private GameObject Messages;
    [SerializeField] private GameObject WifiSignal;
    [SerializeField] private GameObject TaskUI;
    [SerializeField] private GameObject InventoryUI;
    [SerializeField] private GameObject AppIcons;
    [SerializeField] private GameObject Map;
    [SerializeField] private GameObject BackButton;
    [SerializeField] private ScrollRect ScrollRect;
    [Header("Setttings UI")]
    [SerializeField] private GameObject SettingsUI;
    [SerializeField] private GameObject SettingsConfirmQuitUI;

    [Header ("Different text conversations")]
    [SerializeField] private GameObject wife;
    [SerializeField] private GameObject work;
    [SerializeField] private GameObject psychologist;

    [SerializeField] private Transform previousItem;
    [SerializeField] private Transform mainItem;
    [SerializeField] private Transform nextItem;

    [SerializeField] private TextMeshProUGUI taskText;

    private void Awake()
    {
        MessageSystem.onPlayerNotified += HighlightContact;
    }

    private void Start()
    {
        MemoryShard memory = FindObjectOfType<MemoryShard>();
        if(memory != null)
        {
            StoryItemToCollect();
        }
    }
    public void UpdateSignal(int strength)
    {
        WifiSignal.GetComponent<Image>().sprite = signalState[strength];
        player.GetComponent<MessageSystem>().UpdateSignal(strength);
    }

    public void GoToHomeScreen()
    {
        InventoryUI.SetActive(false);
        TaskUI.SetActive(false);
        AppIcons.SetActive(true);
        BackButton.SetActive(false);
        Map.SetActive(false);
        Contacts.SetActive(false);
        Messages.SetActive(false);
        SettingsUI.SetActive(false);

        if (hasNewMessages)
            AppIcons.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);

    }

    public void DisplayContacts()
    {
        AppIcons.SetActive(false);
        Contacts.SetActive(true);
        BackButton.SetActive(true);
        
        AppIcons.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        hasNewMessages = false;

        for (int i = 0; i < Contacts.transform.childCount; i++)
        {
            if (HighlightStatus[i] == true)
                Contacts.transform.GetChild(1).GetChild(i).GetChild(0).gameObject.SetActive(true); // Contacts >> Conversations >> Wife,Work,Psych
            else
                Contacts.transform.GetChild(1).GetChild(i).GetChild(0).gameObject.SetActive(false); // Contacts >> Conversations >> Wife,Work,Psych

        }
    }


    public void DisplayMessages(int conversation)
    {
        Contacts.SetActive(false);
        Messages.SetActive(true);
        BackButton.SetActive(true);

        switch (conversation)
        {
            case 0:
                wife.SetActive(true);
                work.SetActive(false);
                psychologist.SetActive(false);
                player.GetComponent<MessageSystem>().UpdateUser(CurrentUser.WIFE);
                HighlightStatus[0] = false;
                ScrollRect.content = wife.GetComponent<RectTransform>();
                break;
            case 1:
                wife.SetActive(false);
                work.SetActive(true);
                psychologist.SetActive(false);
                player.GetComponent<MessageSystem>().UpdateUser(CurrentUser.WORK);
                HighlightStatus[1] = false;
                ScrollRect.content = work.GetComponent<RectTransform>();
                break;
            case 2:
                wife.SetActive(false);
                work.SetActive(false);
                psychologist.SetActive(true);
                player.GetComponent<MessageSystem>().UpdateUser(CurrentUser.PSYCHOLOGIST);
                HighlightStatus[2] = false;
                ScrollRect.content = psychologist.GetComponent<RectTransform>();
                break;
            default:
                Contacts.SetActive(true);
                Messages.SetActive(false);
                break;
        }
    }

    public void DisplayMap()
    {
        AppIcons.SetActive(false);
        BackButton.SetActive(true);
        Map.SetActive(true);

    }

    public void DisplayTask()
    {
        AppIcons.SetActive(false);

        SetupTaskTest();

        TaskUI.SetActive(true);
        BackButton.SetActive(true);
    }

    public void DisplaySettings() 
    {
        AppIcons.SetActive(false);

        SettingsUI.SetActive(true);
        SettingsConfirmQuitUI.SetActive(false);

        BackButton.SetActive(true);
    }

    public void DisplayQuitConfirm()
    {
        SettingsConfirmQuitUI.SetActive(true);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void SetupTaskTest()
    {
        if (GetComponent<Phone>().CurrentTask != null)
        {
            string taskList = "";
            taskList += SetupBrokenItems(taskList);
            taskList += SetupItemsToClean(taskList);
            taskText.text = taskList;

            StoryItemToCollect();
        }
        else
        {
            taskText.text = "Go to the Task Machine to get your tasks!";
        }
    }

    private string SetupBrokenItems(string taskList)
    {
        List<BrokenItemData> tasks = GetComponent<Phone>().CurrentTask.brokenItems;

        tasks.Sort((x, y) => x.isMainTask.CompareTo(y.isMainTask));
        string task = "";

        for (int i = 0; i < tasks.Count; i++)
        {
            task += "Repair: ";
            task += tasks[i].name + "\nLocation: " + tasks[i].place;

            if (tasks[i].isMainTask)
            {
                task += "\nType: MAIN TASK";
            }
            else
            {
                task += "(Optional):";
            }
            task += " ";
            if (tasks[i].state == BrokenItemState.IsRepaired)
            {
                task += "\nState: DONE";
            }
            else
            {
                task += "\nState: TO DO";
            }

            task += "\n\n";
        }

        return task;
    }

    private string SetupItemsToClean(string taskList)
    {
        List<CleanItemData> tasks = GetComponent<Phone>().CurrentTask.cleanItems;

        tasks.Sort((x, y) => x.isMainTask.CompareTo(y.isMainTask));
        string task = "";

        for (int i = 0; i < tasks.Count; i++)
        {
            task += "Clean: ";
            task += tasks[i].name + "\nLocation: " + tasks[i].place;

            if (tasks[i].isMainTask)
            {
                task += "\nType: MAIN TASK";
            }
            else
            {
                task += "\nType: OPTIONAL";
            }
            task += " ";
            if (tasks[i].state == CleanItemState.Cleaned)
            {
                task += "\nState: DONE";
            }
            else
            {
                task += "\nState: TO DO";
            }

            task += "\n\n";
        }

        return task;
    }

    public void StoryItemToCollect()
    {
        string task = taskText.text.ToString();
        StoryItem storyItem = GetComponent<Phone>().CurrentTask.storyItem;

        if ( storyItem != null && storyItem.state != StoryItemState.Hidden)
        {
            task += "Collect: ";
            task += storyItem.name + "\nLocation: " + storyItem.place;

            if (storyItem.isMainTask)
            {
                task += "\nType: MAIN TASK";
            }
            else
            {
                task += "\nType: OPTIONAL";
            }
            task += " ";
            if (storyItem.state == StoryItemState.Collected)
            {
                task += "\nState: DONE";
            }
            else
            {
                task += "\nState: TO DO";
            }

            task += "\n\n";
        
        }

        taskText.text = task;
    }

    public void DisplayInventory()
    {
        AppIcons.SetActive(false);
        InventoryUI.SetActive(true);
        BackButton.SetActive(true);
        DisplayItems();
    }

    public void HighlightContact(CurrentUser user)
    { 
        switch (user) {
            case CurrentUser.WIFE:
                HighlightStatus[0] = true;
                break;
            case CurrentUser.WORK:
                HighlightStatus[1] = true;
                break;      
            case CurrentUser.PSYCHOLOGIST:
                HighlightStatus[2] = true;
                break;
        }

        hasNewMessages = true;
    }

    //TODO: Optimize code so that object are only deleted when nessecary
    private void DisplayItems()
    {
        //-1 0 +1
        if (previousItem.childCount > 0)
        {
            Destroy(previousItem.transform.GetChild(0).gameObject);
        }
        if (mainItem.childCount > 0)
        {
            Destroy(mainItem.transform.GetChild(0).gameObject);
        }
        if (nextItem.childCount > 0)
        {
            Destroy(nextItem.transform.GetChild(0).gameObject);
        }

        if (inventoryData.Items.Count < 1)
        {
            return;
        }

        for (int i = -1; i < 2; i++)
        {
            int tempIndex = currentItemIndex + i;
            if (tempIndex < 0)
            {
                tempIndex = inventoryData.Items.Count - 1;
            }
            if (tempIndex >= inventoryData.Items.Count)
            {
                tempIndex = 0;
            }
            GameObject item = new GameObject(inventoryData.Items[tempIndex].ObjectName);
            item.transform.localScale = new Vector3(100, 100, 100);
            item.layer = LayerMask.NameToLayer("UI Camera");
            item.AddComponent<MeshRenderer>();
            item.AddComponent<MeshFilter>();
            item.GetComponent<MeshRenderer>().material = inventoryData.Items[tempIndex].material;
            item.GetComponent<MeshFilter>().mesh = inventoryData.Items[tempIndex].mesh;
          
            
            switch (i)
            {
                case -1:
                    item.transform.parent = previousItem;
                    break;
                case 0:
                    item.transform.parent = mainItem;
                    break;
                case 1:
                    item.transform.parent = nextItem;
                    break;

            }
            item.transform.localPosition = Vector3.zero;
        }
    }

    private void RemoveItem(GameObject obj)
    {
        Destroy(obj);
    }

    public void NextItem()
    {
        Debug.Log("Next");
        currentItemIndex++;
        if (currentItemIndex >= inventoryData.Items.Count)
        {
            currentItemIndex = 0;
        }

        DisplayItems();
    }

    public void PreviousItem()
    {
        Debug.Log("Previous");
        currentItemIndex--;
        if (currentItemIndex < 0)
        {
            currentItemIndex = inventoryData.Items.Count-1;
        }
        DisplayItems();
    }
}
