using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReference : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera m_camera;
    [SerializeField]
    private bool isTpsCamera = false;
    public CinemachineVirtualCamera Camera { get => m_camera; }

    private void Start()
    {
        if(isTpsCamera)
        {
            m_camera = GameObject.Find("TPS-Camera").GetComponent<CinemachineVirtualCamera>();
        }
    }

}
