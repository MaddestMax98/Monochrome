using ScripatbleObj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanItemInventory : MonoBehaviour
{
    [SerializeField]
    private CleanItemInventoryData inventoryData;

    private static CleanItemInventory instance;
    public static CleanItemInventory Instance { get => instance; set => instance = value; }

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
