using Unity.VisualScripting;
using UnityEngine;

namespace ScripatbleObj
{
    [CreateAssetMenu(fileName = "MannequinItemData", menuName = "Scriptable Objects/Game/Objects/Interactable")]
    public class MannequinItemData : BaseObjectData
    {
        public GameObject obj;
        public bool isEquiped = false;
    }
}