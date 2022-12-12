using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is just used on virtual cameras so they can target a gameobject at runtime
//Example spawned player
public class VirtualCameraTarget : MonoBehaviour
{
    private Transform player;


    private void Start()
    {
        player = GameObject.Find("Player(Clone)").transform;
        GetComponent<CinemachineVirtualCamera>().LookAt = player;
    }
}
