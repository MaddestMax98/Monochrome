using UnityEngine;
using TMPro;
using ScripatbleObj;
using UnityEngine.UI;

public class PhoneUI : MonoBehaviour
{
    [SerializeField]
    private PlayerInventoryData inventoryData;
    private int currentItemIndex = 0;

    [SerializeField] private Sprite[] signalState;
    
    [SerializeField] private GameObject WifiSignal;
    [SerializeField] private GameObject TaskUI;
    [SerializeField] private GameObject InventoryUI;
    [SerializeField] private GameObject AppIcons;
    [SerializeField] private GameObject Messages;
    [SerializeField] private GameObject Map;
    [SerializeField] private GameObject BackButton;

    [SerializeField] private Transform previousItem;
    [SerializeField] private Transform mainItem;
    [SerializeField] private Transform nextItem;

    private void OnEnable()
    {
        AppIcons.SetActive(true);
        TaskUI.SetActive(false);
        InventoryUI.SetActive(false);
        BackButton.SetActive(false);
    }

    public void UpdateSignal(int strength)
    {
        WifiSignal.GetComponent<Image>().sprite = signalState[strength];
    }

    public void GoToHomeScreen()
    {
        InventoryUI.SetActive(false);
        TaskUI.SetActive(false);
        AppIcons.SetActive(true);
        BackButton.SetActive(false);
        Map.SetActive(false);
        Messages.SetActive(false);
    }

    public void DisplayMessages()
    {
        AppIcons.SetActive(false);
        Messages.SetActive(true);
        BackButton.SetActive(true);
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
        TaskUI.SetActive(true);
        BackButton.SetActive(true);
    }

    public void DisplayInventory()
    {
        AppIcons.SetActive(false);
        InventoryUI.SetActive(true);
        BackButton.SetActive(true);
        DisplayItems();
    }

    //TODO: Optimize code so that object are only deleted when nessecary
    private void DisplayItems()
    {
        if (inventoryData.Items.Count < 1)
        {
            return;
        }

        //-1 0 +1
        if (previousItem.transform.childCount > 0)
        {
            Destroy(previousItem.transform.GetChild(0).gameObject);
        }
        if (mainItem.transform.childCount > 0)
        {
            Destroy(mainItem.transform.GetChild(0).gameObject);
        }
        if (nextItem.transform.childCount > 0)
        {
            Destroy(nextItem.transform.GetChild(0).gameObject);
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
        currentItemIndex++;
        if (currentItemIndex >= inventoryData.Items.Count)
        {
            currentItemIndex = 0;
        }

        DisplayItems();
    }

    public void PreviousItem()
    {
        currentItemIndex--;
        if (currentItemIndex < 0)
        {
            currentItemIndex = inventoryData.Items.Count-1;
        }
        DisplayItems();
    }
}
