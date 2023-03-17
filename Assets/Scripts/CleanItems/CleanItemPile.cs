using ScripatbleObj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanItemPile : Interactable
{
    //What items can be placed in this pile
    [SerializeField]
    private string pile = "";

    public override void Interact()
    {
        PlayerInventoryData playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().Inventory;

        for (int i = 0; i < playerInventory.Items.Count; i++)
        {
            CleanItemData data = playerInventory.Items[i] as CleanItemData;
            if (data.pile == pile)
            {
                data.isUsed = true;
                data.state = CleanItemState.Cleaned;
                playerInventory.Items.Remove(playerInventory.Items[i]);
            }
        }
    }
}
