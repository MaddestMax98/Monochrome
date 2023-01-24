using PlayerCharacter;
using ScripatbleObj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenItem : Interactable
{
    [SerializeField]
    private BrokenItemData brokenItem;

    bool isRepaired; //needed?



    void SetupObject()
    {
        //Is it part of the task
        //Has it been repaired
        //Should it cascade
    }

    public override void Interact()
    {
        PlayerInventory p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        bool hasItems = true;

        for (int i = 0; i < brokenItem.itemsNeededToRepair.Length; i++)
        {
            if (!p.Inventory.Items.Contains(brokenItem.itemsNeededToRepair[i]))
            {
                hasItems = false;
                break;
            }
        }

        if (hasItems)
        {
            RepairObject();
        }

    }

    private void RepairObject()
    {
        Debug.Log("Repairing");
    }
}
