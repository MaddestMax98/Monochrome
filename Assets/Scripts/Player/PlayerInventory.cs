using ScripatbleObj;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private static PlayerInventory instance;

    [SerializeField]
    private PlayerInventoryData inventory;

    public static PlayerInventory Instance { get => instance; set => instance = value; }
    public PlayerInventoryData Inventory { get => inventory; set => inventory = value; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
