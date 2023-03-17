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

    public TaskData GetDayTask()
    {
        return daysList.taskList[currentDay - 1];
    }

    public bool IsMainTasksDone()
    {
        bool isDone = true;
        Debug.Log(daysList.taskList[1].brokenItems.Count);
        //If one of the main tasks is not done we break through the loop and return false


        for (int i = 0; i < daysList.taskList[currentDay-1].brokenItems.Count; i++)
        {
            if (daysList.taskList[currentDay - 1].brokenItems[i].isMainTask && daysList.taskList[currentDay - 1].brokenItems[i].state != BrokenItemState.IsRepaired)
            {
                isDone = false;
                break;
            }
        }

        return isDone;
    }

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

    public CleanItemData[] GetCurrentDayCleanItems()
    {
        return daysList.taskList[currentDay - 1].cleanItems.ToArray(); //Arrays start at zero!
    }
}
