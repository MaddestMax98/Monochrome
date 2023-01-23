using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScripatbleObj
{
    [CreateAssetMenu(fileName = "TaskListData", menuName = "Scriptable Objects/Game/TaskList")]
    public class TaskListData : ScriptableObject
    {
        public List<TaskData> taskList;
    }

}
