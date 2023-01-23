using PlayerCharacter;
using ScripatbleObj;
using UnityEngine;

public class TaskMachine : Interactable
{
    private int tempDay = 1;
    [SerializeField]
    private TaskData[] tasks;

    public TaskData GetTask()
    {
        return tasks[tempDay-1];
    }

    public override void Interact()
    {
        Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        p.Phone.CurrentTask = GetTask();
    }
}
