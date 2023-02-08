using UnityEngine;
using ScripatbleObj;
using Manager;

namespace Item
{
    public class PickupUsableItem : PickupItem
    {
        [SerializeField]
        private UsableItemData itemData;
        [SerializeField]
        private PlayerInventoryData playerInventoryData;

        public override void Start()
        {
            base.Start();
            if (itemData.isPickedUp)
            {
                Destroy(gameObject);
            }
        }

        public override void Pickup()
        {
            itemData.isPickedUp = true;
            playerInventoryData.Add(itemData);
            Destroy(gameObject);
        }

    }
}
