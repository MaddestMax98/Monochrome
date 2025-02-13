using ScripatbleObj;
using UnityEngine;

public class MannequinItem : MonoBehaviour
{
    [SerializeField]
    private MannequinItemData itemData;
    private Transform wardrobeParent;
    public MannequinItemData ItemData { get => itemData; set => itemData = value; }
    public Transform WardrobeParent { get => wardrobeParent; set => wardrobeParent = value; }
}
