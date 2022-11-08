using UnityEngine;

namespace ScripatbleObj
{
    public abstract class BaseObjectData : ScriptableGameObject
    {
        [Tooltip("Specify the type of this object")]
        public ItemType Type;
    }
}

