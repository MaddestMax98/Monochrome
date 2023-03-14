using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingSetUp: MonoBehaviour
{
    private static PostProcessingSetUp instance;
    public static PostProcessingSetUp Instance { get => instance; set => instance = value; }

    [SerializeField]
    private BooleanType _isActive;

    private Volume _volume;
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

        _volume = GetComponent<Volume>();
        UpdatePostProcess();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {

            if (_isActive.value)
            {
                _isActive.value = false;
                UpdatePostProcess();
            }
                
            else
            {
                _isActive.value = true;
                UpdatePostProcess();
            } 
        }
    }

    private void UpdatePostProcess()
    {
        _volume.enabled = _isActive.value;
    }
}
