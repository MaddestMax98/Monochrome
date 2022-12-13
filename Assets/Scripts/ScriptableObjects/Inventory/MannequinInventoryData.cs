using System.Collections.Generic;
using UnityEngine;

namespace ScripatbleObj
{
    [CreateAssetMenu(fileName = "MannequinInventoryData", menuName = "Scriptable Objects/Game/Objects/Inventory")]
    public class MannequinInventoryData : ScriptableObject
    {
        [SerializeField]
        public List<MannequinItemData> Items;

        public MannequinItemData GetItem()
        {
            return null;
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
