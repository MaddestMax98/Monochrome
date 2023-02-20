using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects {

    public class AnomalyHandlerData : ScriptableObject
    {
        public List<AnomalyData> anomalies;
        public int currentAnomalies;
    }
}
