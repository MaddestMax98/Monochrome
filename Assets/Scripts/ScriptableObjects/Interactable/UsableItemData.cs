using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScripatbleObj
{
    [CreateAssetMenu(fileName = "UsableItemData", menuName = "Scriptable Objects/Game/Objects/UsableItem")]
    public class UsableItemData : BaseObjectData
    {
        public Mesh mesh;
        public Material material;
        public bool isPickedUp = false;
        public bool isUsed = false;
    }
}
