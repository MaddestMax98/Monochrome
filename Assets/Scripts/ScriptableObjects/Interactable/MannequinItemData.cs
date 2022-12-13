using Unity.VisualScripting;
using UnityEngine;

namespace ScripatbleObj
{
    [CreateAssetMenu(fileName = "MannequinItemData", menuName = "Scriptable Objects/Game/Objects/Interactable")]
    public class MannequinItemData : BaseObjectData
    {
        public Mesh mesh;
        public Material material;
        public bool isPickedUp= false;
        public bool isEquiped = false;
    }
}