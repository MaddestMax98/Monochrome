using UnityEngine;

namespace AnomalySystem.ScriptableObjects {

    public class AnomalyStoringData : ScriptableObject
    {
        // -- Saving position --
        [Header ("Saved Positions")]
        public Vector3 originalPos;
        public Vector3 currentPos;
        
        // -- Saving Rotation
        [Header("Saved Quaternion Rotation")]
        public Quaternion originalRot;
        public Quaternion currentRot;

        // -- Saving Scale --
        [Header("Saved Local Scale")]
        public Vector3 originalScale;
        public Vector3 currentScale;

        public bool isActive;
    }
}
