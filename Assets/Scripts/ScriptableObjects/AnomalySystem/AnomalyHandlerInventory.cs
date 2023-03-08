using System.Collections.Generic;
using UnityEngine;

namespace AnomalySystem.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AnomalyHandlerInventory", menuName = "Scriptable Objects/Game/Objects/Inventory/AnomalyHandler")]
    public class AnomalyHandlerInventory : ScriptableObject
    {
        public List<AnomalyHandlerData> data;
    }
}

