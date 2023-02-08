using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScripatbleObj
{
    [CreateAssetMenu(fileName = "PlayerInventoryData", menuName = "Scriptable Objects/Game/Objects/Inventory/Player")]
    public class PlayerInventoryData : ScriptableObject
    {
        [SerializeField]
        public List<UsableItemData> Items;

        public void Add(UsableItemData item)
        {
            Items.Add(item);
        }

        public void Remove(UsableItemData item)
        {
            Items.Remove(item);
        }

        public void ResetInventory()
        {
            Debug.Log("Reseting player inventory");
            Items.Clear();
        }
    }
}
