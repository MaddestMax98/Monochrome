using ScripatbleObj;
using UnityEngine;

public class BrokenItem : Interactable
{
    [SerializeField]
    private BrokenItemData brokenItem;

    public BrokenItemData Data { get => brokenItem; }

    /*---------------Setup Object---------------*/
    public override void Start()
    {
        if (brokenItem.state != BrokenItemState.NotImportant && brokenItem.state != BrokenItemState.IsRepaired)
        {
            base.Start();
        }


        if (brokenItem.state == BrokenItemState.Cascade)
        {
            GameObject cascade = Instantiate(brokenItem.prefabCascade, this.transform);
            cascade.transform.localPosition = Vector3.zero;
            cascade.transform.localRotation = Quaternion.Euler(0, 0, 0);
            cascade.transform.localScale = Vector3.one;

        }

    }

    public override void Interact()
    {
        if (brokenItem.state == BrokenItemState.CurrentTask || brokenItem.state == BrokenItemState.Cascade)
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
            return;
        }

    }

    private void RepairObject()
    {
        if (brokenItem.state == BrokenItemState.Cascade)
        {
            Destroy(this.transform.GetChild(0).gameObject);
        }
        brokenItem.state = BrokenItemState.IsRepaired;
        Debug.Log("Repaired");
    }
}
