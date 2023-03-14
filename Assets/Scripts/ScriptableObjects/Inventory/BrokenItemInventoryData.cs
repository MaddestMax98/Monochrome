using System.Collections.Generic;
using UnityEngine;

namespace ScripatbleObj
{
    [CreateAssetMenu(fileName = "BrokenInventory", menuName = "Scriptable Objects/Game/Objects/Inventory/BrokenItemInventory")]
    public class BrokenItemInventoryData : ScriptableObject
    {
        public List<BrokenItemData> data;
    }
}

