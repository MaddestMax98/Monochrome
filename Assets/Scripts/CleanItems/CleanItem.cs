using PlayerCharacter;
using ScripatbleObj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanItem : Interactable
{
    [SerializeField]
    private CleanItemData itemData;

    public CleanItemData ItemData { get => itemData; set => itemData = value; }

    public override void Start()
    {
        if (ItemData.state == CleanItemState.NotImportant || ItemData.state == CleanItemState.Cleaned || itemData.isPickedUp)
        {
            Destroy(gameObject);
        }
    }

    public override void Interact()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (!ItemData.isMainTask)
        {
            ItemData.state = CleanItemState.Cleaned;
            player.SetSanity(1);
        }
        else
        {
            //Add to player inventory
            player.GetComponent<PlayerInventory>().Inventory.Add(itemData);
        }
        itemData.isPickedUp = true;
        Destroy(gameObject);
    }
}
