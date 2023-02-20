using UnityEngine;

namespace AnomalySystem.ScriptableObjects 
{
    [CreateAssetMenu(fileName = "AnomalyData", menuName = "Scriptable Objects/Game/AnomalySystem/AnomalyData")]
    public class AnomalyData : ScriptableObject
    {
        public string anomalyName;
        public string description;
        public AnomalyType aType = AnomalyType.STANDARD;
        public AudioSource staticSound;
        public AudioSource triggered;
        public AudioSource triggeredNextToPlayer;
    }
}


