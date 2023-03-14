using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneSetUp : MonoBehaviour
{
    [SerializeField] private PhoneUI _phone;
    private void OnEnable()
    {
        _phone.GoToHomeScreen();
    }
}
