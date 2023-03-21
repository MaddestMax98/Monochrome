using UnityEngine;

namespace ScriptableObjects{
    [CreateAssetMenu(fileName = "MessageStorageData", menuName = "Scriptable Objects/PhoneUI/MessageStorage")]
    public class MessageStorageData : ScriptableObject
    {
        public int totalWifeRespones = 0;
        public int totalWorkRespones = 0;
        public int totalPsychRespones = 0;

        public bool isScenePersistenceLinked = false;
        public bool isWaitingForResponse = false;
    }
}


