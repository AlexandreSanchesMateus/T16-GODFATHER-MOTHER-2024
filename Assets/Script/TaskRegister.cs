using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskRegister : MonoBehaviour
{
    [SerializeField]
    private SCO_Tasks SCO_Ref;

    [SerializeField, Foldout("Tasks")]
    private List<Task> leftTasks = new List<Task>();
    [SerializeField, Foldout("Tasks")]
    private List<Task> middleTasks = new List<Task>();
    [SerializeField, Foldout("Tasks")]
    private List<Task> rightTasks = new List<Task>();

    void Start()
    {
        if (!SCO_Ref)
        {
            Debug.LogError("Aucune instance de SCO_Tasks est donnée.");
            return;
        }

        SCO_Ref.SetRegister(leftTasks, middleTasks, rightTasks);
        Debug.Log("TaskRegister - OK");
    }
}
