using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusChanger : MonoBehaviour
{
    [SerializeField] public GameObject _read;
    [SerializeField] private GameObject _recieved;
    [SerializeField] private GameObject _sent;

    public delegate void PlayerAnswered();
    public static PlayerAnswered onPlayerAnswer;

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
        if (current >= 3)
        {
            hasResponded = true;
            gameObject.GetComponentInChildren<TextMeshProUGUI>().alpha = 1.0f;
            gameObject.GetComponent<Button>().interactable = false;

            if (onPlayerAnswer != null)
                onPlayerAnswer?.Invoke();
        }
    }
}
