using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnomalySystem.ScriptableObjects {
    [CreateAssetMenu(fileName = "AnomalyHandlerData", menuName = "Scriptable Objects/Game/AnomalySystem/AnomalyHandlerData")]
    public class AnomalyHandlerData : ScriptableObject
    {
        public List<AnomalyStoringData> anomalies;
        public int currentAnomalies;
    }
}
