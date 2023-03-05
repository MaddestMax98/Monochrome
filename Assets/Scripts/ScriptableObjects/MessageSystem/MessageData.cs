using UnityEngine;

namespace ScriptableObjects{
    [CreateAssetMenu(fileName = "MessageData", menuName = "Scriptable Objects/PhoneUI/Messages")]
    public class MessageData : ScriptableObject
    {
        public string[] texts;
        public int current = 0;
    }
}


