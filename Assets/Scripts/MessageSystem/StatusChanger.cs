using UnityEngine;

public class StatusChanger : MonoBehaviour
{
    [SerializeField] public GameObject _read;
    [SerializeField] private GameObject _recieved;
    [SerializeField] private GameObject _sent;

    public void ChangeStatus(int signalStrength)
    {
        switch (signalStrength) {
            case 3: 
                _read.SetActive(true);
                _recieved.SetActive(false);
                _sent.SetActive(false);
                break;
            case 2:
                _read.SetActive(false);
                _recieved.SetActive(true);
                _sent.SetActive(false);
                break;
            default:
                _read.SetActive(false);
                _recieved.SetActive(false);
                _sent.SetActive(true);
                break;
        }

    }
}
