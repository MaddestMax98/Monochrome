using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScripatbleObj
{
    [CreateAssetMenu(fileName = "BrokenItemData", menuName = "Scriptable Objects/Game/Objects/BrokenItem")]
    public class BrokenItemData : ScriptableObject
    {
        public bool isRepaired;
        public BrokenItemType brokenType;
        public UsableItemData[] itemsNeededToRepair;
    }
}
