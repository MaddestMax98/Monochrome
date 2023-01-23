using PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenItem : Interactable
{
    [SerializeField]
    private CascadeEffect.CascadeEffectType effectType;
    [SerializeField]
    private string[] ItemsToRepair;
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
        int hasItemCounter = 0;
        for (int i = 0; i < p.Inventory.Items.Count; i++)
        {
            for (int j = 0; j < ItemsToRepair.Length; j++)
            {
                if (ItemsToRepair[j] == p.Inventory.Items[i].ObjectName)
                {
                    hasItemCounter++;
                }
            }
        }

        if (hasItemCounter == ItemsToRepair.Length)
        {
            RepairObject();
        }
    }

    private void RepairObject()
    {

    }
}
