using ScripatbleObj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryItemInventory : MonoBehaviour
{
    [SerializeField]
    private StoryItemInventoryData inventoryData;

    private static StoryItemInventory instance;
    public static StoryItemInventory Instance { get => instance; set => instance = value; }

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
