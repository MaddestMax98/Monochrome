using System.Collections.Generic;
using UnityEngine;

namespace ScripatbleObj
{
    [CreateAssetMenu(fileName = "MannequinInventoryData", menuName = "Scriptable Objects/Game/Objects/Inventory")]
    public class MannequinInventoryData : ScriptableGameObject
    {
        [SerializeField]
        public List<MannequinInventoryItem> Items;

        [SerializeField]
        private GameEvent OnAdd; //raised whenever we increment the count for an item (e.g. health)

        [SerializeField]
        private GameEvent OnEquip;

        public void EquipItem(MannequinItemData interactableObjectData)
        {
            var index = Items.FindIndex(inventoryItem => inventoryItem.InteractableObjectData == interactableObjectData);

            switch (Items[index].InteractableObjectData.Type)
            {
                case ItemType.Hat:

                    break;
                case ItemType.Jacket:

                    break;
                case ItemType.Pants:

                    break;
                case ItemType.Shoes:

                    break;
            }


            Items[index].InteractableObjectData.isEquiped = true;

            OnEquip?.Raise();

            //Debug.Log(Items[index].InteractableObjectData.isEquiped);

        }

        public void UnEquipItem(MannequinItemData interactableObjectData)
        {
            Debug.Log(interactableObjectData.Type);
        }

        public void AddItem(MannequinItemData interactableObjectData)
        {
            //find the inventory slot for the item
            var index = Items.FindIndex(inventoryItem => inventoryItem.InteractableObjectData == interactableObjectData);

            Items.Add(new MannequinInventoryItem(interactableObjectData));
            //notify listeners
            OnAdd?.Raise();
            Debug.Log(Items[0]);
        }
    }

    [System.Serializable]
    public class MannequinInventoryItem
    {
        public MannequinItemData InteractableObjectData;

        public MannequinInventoryItem(MannequinItemData interactableObjectData)
        {
            InteractableObjectData = interactableObjectData;
        }
    }
}
