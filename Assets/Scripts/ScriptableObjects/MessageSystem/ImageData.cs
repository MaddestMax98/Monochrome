using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ImageData", menuName = "Scriptable Objects/PhoneUI/ImageSMS")]
    public class ImageData : ScriptableObject
    {
        public Sprite[] images;
        public int current = 0;
    }
}
