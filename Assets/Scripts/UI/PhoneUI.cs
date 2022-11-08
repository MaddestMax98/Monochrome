using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhoneUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI signal;

    public void UpdateSignal(int strength)
    {
        signal.text = strength.ToString();
    }
}
