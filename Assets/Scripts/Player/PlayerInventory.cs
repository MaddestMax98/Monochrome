using ScripatbleObj;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private PlayerInventoryData inventory;

    public PlayerInventoryData Inventory { get => inventory; set => inventory = value; }
}
