using ScripatbleObj;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DaySystem : MonoBehaviour
{
    int currentDay = 2;

    [SerializeField]
    private TaskListData daysList;

    public BrokenItemData[] GetCurrentDayBrokenItems()
    {
        return daysList.taskList[currentDay-1].brokenItems.ToArray(); //Arrays start at zero!
    }

    public BrokenItemData[] GetPreviousDayBrokenItems()
    {
        if (currentDay - 2 >= 0)
        {
            return daysList.taskList[currentDay - 2].brokenItems.ToArray(); //Arrays start at zero!
        }
        return null;
    }
}
