using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct BrokenItemObject
{
    public int id;
    public bool isRepaired;

    public BrokenItemObject(int id, bool isRepaired)
    {
        this.id = id;
        this.isRepaired = isRepaired;
    }
}

namespace ScripatbleObj
{
    [CreateAssetMenu(fileName = "TaskData", menuName = "Scriptable Objects/Game/Task")]
    public class TaskData : ScriptableObject
    {
        public int day;
        public string MainTaskDescription;
        public bool mainTaskDone = false;
        public List<BrokenItemObject> brokenItems;
    }

}
