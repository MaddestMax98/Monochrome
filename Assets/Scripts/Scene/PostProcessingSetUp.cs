using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingSetUp: MonoBehaviour
{
    [SerializeField]
    private BooleanType _isActive;

    private Volume _volume;
    private void Awake()
    {
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
