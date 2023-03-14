using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScripatbleObj
{
    [CreateAssetMenu(fileName = "MessageInventory", menuName = "Scriptable Objects/Game/Objects/Inventory/MessageInventory")]
    public class MessageInventoryData : ScriptableObject
    {
        public List<MessageData> messageData;
        public List<ImageData> imageData;
    }
}

