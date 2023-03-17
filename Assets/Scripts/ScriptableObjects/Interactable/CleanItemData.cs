using UnityEngine;

namespace ScripatbleObj
{
    [CreateAssetMenu(fileName = "CleanItemData", menuName = "Scriptable Objects/Game/Objects/CleanItem")]
    public class CleanItemData : UsableItemData
    {
        [Header("Clean Item Data")]
        public bool isMainTask = false;
        public string pile = "";
        public CleanItemState state = CleanItemState.NotImportant;
        public string place = "";
    }
}

