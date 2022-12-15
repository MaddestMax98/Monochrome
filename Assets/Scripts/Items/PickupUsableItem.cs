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
        public override void Pickup()
        {
            playerInventoryData.Add(itemData);
            Destroy(gameObject);
        }

    }
}
