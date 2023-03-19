using UnityEngine;

namespace ScripatbleObj
{
    [CreateAssetMenu(fileName = "StoryItem", menuName = "Scriptable Objects/Game/Objects/StoryItem")]
    public class StoryItem : ScriptableObject
    {
        public bool isMainTask = false;
        public string place = "";
        public StoryItemState state = StoryItemState.Hidden;
    }
}
