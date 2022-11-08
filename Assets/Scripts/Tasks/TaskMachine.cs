using ScripatbleObj;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMachine : MonoBehaviour
{
    private int tempDay = 1;
    [SerializeField]
    private TaskData[] tasks;

    public TaskData GetTask()
    {
        return tasks[tempDay-1];
    }
}
