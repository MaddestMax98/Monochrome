using UnityEngine;

namespace AnomalySystem.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AnomalyScenePersistance", menuName = "Scriptable Objects/Game/AnomalySystem/ScenePersistance")]
    public class AnomalyScenePersistance : ScriptableObject
    {
        public bool wasTriggered = false;
        public bool hasUpdated = false;
        public Transform currentTransform;
    }
}

