using UnityEngine;

public class StatusChanger : MonoBehaviour
{
    [SerializeField] public GameObject _read;
    [SerializeField] private GameObject _recieved;
    [SerializeField] private GameObject _sent;

    int current = 0;
    bool hasResponded = false;

    public void ChangeStatus(int signalStrength)
    {
        current = signalStrength;

        switch (signalStrength) {
            case 4:
                _read.SetActive(true);
                _recieved.SetActive(false);
                _sent.SetActive(false);
                break;
            case 2: case 3:
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

    public int GetStatus()
    {
        return current;
    }

    public bool HasResponded()
    {
        return hasResponded;
    }

    public void Answer()
    {
        hasResponded = true;
    }
}
