using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScripatbleObj
{
    [CreateAssetMenu(fileName = "StoryItemInventory", menuName = "Scriptable Objects/Game/Objects/Inventory/StoryItemInventory")]
    public class StoryItemInventoryData : ScriptableObject
    {
        public List<StoryItem> data;
    }
}

