using ScripatbleObj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaySystem : MonoBehaviour
{
    int currentDay = 1;

    [SerializeField]
    private TaskListData daysList;

    public BrokenItemData[] GetBrokenItems()
    {
        return daysList.taskList[currentDay].brokenItems.ToArray();
    }
}
