using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScripatbleObj
{
    [CreateAssetMenu(fileName = "TaskData", menuName = "Scriptable Objects/Game/Task")]
    public class TaskData : ScriptableObject
    {
        public int day;
        public string MainTaskDescription;
        public bool mainTaskDone = false;
        public List<BrokenItemData> brokenItems;
    }

}
