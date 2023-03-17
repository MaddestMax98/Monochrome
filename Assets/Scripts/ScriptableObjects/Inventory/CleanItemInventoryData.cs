using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScripatbleObj
{
    [CreateAssetMenu(fileName = "CleanItemInventory", menuName = "Scriptable Objects/Game/Objects/Inventory/CleanItemInventory")]
    public class CleanItemInventoryData : ScriptableObject
    {
        public List<CleanItemData> data;
    }
}

