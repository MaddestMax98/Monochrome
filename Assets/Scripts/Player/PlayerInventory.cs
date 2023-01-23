using ScripatbleObj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private PlayerInventoryData inventory;

    public PlayerInventoryData Inventory { get => inventory; set => inventory = value; }
}
