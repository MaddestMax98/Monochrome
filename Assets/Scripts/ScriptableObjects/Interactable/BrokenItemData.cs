using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScripatbleObj
{
    [CreateAssetMenu(fileName = "BrokenItemData", menuName = "Scriptable Objects/Game/Objects/BrokenItem")]
    public class BrokenItemData : ScriptableObject
    {
        public BrokenItemState state = BrokenItemState.NotImportant;
        public GameObject prefabCascade;
        public UsableItemData[] itemsNeededToRepair;
    }
}
