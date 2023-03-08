using TMPro;
using UnityEngine;

public class UpdateDate : MonoBehaviour
{
    public void UpdateCurrentDate(string date)
    {
         GetComponent<TextMeshProUGUI>().text =  date;
    }
}
