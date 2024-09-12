using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField, Required]
    private SCO_Tasks SCO_Ref;

    [SerializeField]
    private List<Palier> stages = new List<Palier>();

    public static int CurrentScore {  get; private set; } = 0;
    public static int NbTaskFail { get; private set; } = 0;


    private void Start()
    {
        foreach(Task t in SCO_Ref.AllTasks)
        {
            t.onTaskFinished += TaskFinished;
            t.onTaskFailed += TaskFailed;
        }
    }

    private void TaskFinished(Task other)
    {
        CurrentScore += other.TaskWorth;

        // Voir à la fin de partie
        // Changement de scène
    }

    private void TaskFailed(Task other)
    {
        ++NbTaskFail;
        // Vérifier palier
        CheckStage();
    }

    private void CheckStage()
    {
        if (stages.Count <= 0)
            return;

        if (stages[0].valueToReach <= NbTaskFail)
        {
            stages[0].eventAction?.Invoke();
            stages.RemoveAt(0);
            CheckStage();
        }
    }

    private void OnDestroy()
    {
        foreach (Task t in SCO_Ref.AllTasks)
        {
            t.onTaskFinished += TaskFinished;
            t.onTaskFailed += TaskFailed;
        }
    }
}
