using ScripatbleObj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenItemInventory : MonoBehaviour
{
    private static BrokenItemInventory instance;
    public static BrokenItemInventory Instance { get => instance; set => instance = value; }

    [SerializeField]
    private BrokenItemInventoryData inventory;

    public BrokenItemInventoryData Inventory { get => inventory; set => inventory = value; }

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
