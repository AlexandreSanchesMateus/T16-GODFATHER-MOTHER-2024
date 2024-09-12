using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskRegister : MonoBehaviour
{
    [SerializeField, Required]
    private SCO_Tasks SCO_Ref;
    [SerializeField]
    private List<Task> Tasks = new List<Task>();

    void Awake()
    {
        if (!SCO_Ref)
        {
            Debug.LogError("Aucune instance de SCO_Tasks est donnée.");
            return;
        }

        SCO_Ref.SetRegister(Tasks);
        Debug.Log("TaskRegister - OK");
    }
}
