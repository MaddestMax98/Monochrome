using ScripatbleObj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageInventory : MonoBehaviour
{
    [SerializeField]
    private MessageInventoryData inventoryData;

    private static MessageInventory instance;
    public static MessageInventory Instance { get => instance; set => instance = value; }

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
