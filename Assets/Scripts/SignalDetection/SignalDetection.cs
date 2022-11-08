using PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalDetection : MonoBehaviour
{
    [SerializeField]
    private int signalStrength = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Phone>() != null)
        {
            other.GetComponent<Phone>().Signal = signalStrength;
            other.GetComponent<Phone>().UpdateSignal();
        }
    }
}
