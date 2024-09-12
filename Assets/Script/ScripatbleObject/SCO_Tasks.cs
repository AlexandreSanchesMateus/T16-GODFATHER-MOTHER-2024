using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SCO_Tasks", menuName = "ScriptableObjects/SCO_Tasks", order = 1)]
public class SCO_Tasks : ScriptableObject
{
    public List<Task> AllTasks { get; private set; }


    public List<Task> LeftTasks { get; private set; }
    public List<Task> RightTasks { get; private set; }
    public List<Task> MiddleTasks { get; private set; }

    public void SetRegister(List<Task> listTasks)
    {
        AllTasks = listTasks;

        foreach (Task task in AllTasks)
        {
            switch (task.CamRotState)
            {
                case CamRotationManager.ECamRotState.LEFT:
                    LeftTasks.Add(task);
                    break;

                case CamRotationManager.ECamRotState.MIDDLE:
                    MiddleTasks.Add(task);
                    break;

                case CamRotationManager.ECamRotState.RIGHT:
                    RightTasks.Add(task);
                    break;
            }
        }
    }
}
