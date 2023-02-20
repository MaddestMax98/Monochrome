using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects {

    public class AnomalyData : ScriptableObject
    {
        public Transform originalPos;
        public Transform currentPos;
        public bool isActive;
    }
}
