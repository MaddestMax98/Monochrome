using System.Collections.Generic;
using UnityEngine;

namespace ScripatbleObj
{
    [CreateAssetMenu(fileName = "MannequinInventoryData", menuName = "Scriptable Objects/Game/Objects/Inventory/Mannequin")]
    public class MannequinInventoryData : ScriptableObject
    {
        [SerializeField]
        public List<MannequinItemData> Items;


        public void ResetInventory()
        {
            Items.Clear();
        }
    }
}
