using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "BooleanType", menuName = "Scriptable Objects/Types/Boolean")]
    public class BooleanType : ScriptableObject
    {
        public bool value;
    }
}
