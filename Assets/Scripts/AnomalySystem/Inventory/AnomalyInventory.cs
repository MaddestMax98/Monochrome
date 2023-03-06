using AnomalySystem.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnomalyInventory : MonoBehaviour
{
    [SerializeField]
    private AnomalyHandlerInventory inventory;

    private static AnomalyInventory instance;
    public static AnomalyInventory Instance { get => instance; set => instance = value; }
    public AnomalyHandlerInventory Inventory { get => inventory; set => inventory = value; }

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
