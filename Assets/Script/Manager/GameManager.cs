using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField, BoxGroup("Init"), Required]
    private SCO_Tasks SCO_Ref;
    [SerializeField, BoxGroup("Init"), Required]
    private CamRotationManager camRotationManager;
    [SerializeField, BoxGroup("Init")]
    private TextMeshProUGUI taskFailedTxt;
    [SerializeField, BoxGroup("Init")]
    private TextMeshProUGUI moneyEarnedTxt;

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

    /*private void Update()
    {
        
    }*/

    private void TaskFinished(Task other)
    {
        CurrentScore += other.TaskWorth;
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

    public void LostGame()
    {
        // Retirer la fonctionnalité de tourner
        camRotationManager.CanTriggerEvents = false;

        foreach(Task t in SCO_Ref.AllTasks)
        {
            t.Deactivate();
        }

        // Afficher score
        moneyEarnedTxt.transform.parent.gameObject.SetActive(true);
        moneyEarnedTxt.text = "Argent Gagnés :  " + CurrentScore;
        taskFailedTxt.text = "Tâches échoués : " + NbTaskFail;
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
