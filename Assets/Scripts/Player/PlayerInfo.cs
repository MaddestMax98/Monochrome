using ScripatbleObj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    private static PlayerInfo instance;

    public static PlayerInfo Instance { get => instance; set => instance = value; }
    public PlayerData PlayerData { get => playerData; set => playerData = value; }

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
