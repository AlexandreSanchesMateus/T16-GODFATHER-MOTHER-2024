using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SCO_Tasks", menuName = "ScriptableObjects/SCO_Tasks", order = 1)]
public class SCO_Tasks : ScriptableObject
{
    public List<Task> LeftTasks { get; private set; }
    public List<Task> MiddleTasks { get; private set; }
    public List<Task> RightTasks { get; private set; }

    public void SetRegister(List<Task> leftTasks, List<Task> middleTasks, List<Task> rightTasks)
    {
        LeftTasks = leftTasks;
        MiddleTasks = middleTasks;
        RightTasks = rightTasks;
    }
}
