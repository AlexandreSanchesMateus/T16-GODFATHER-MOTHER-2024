using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField, BoxGroup("Scene Reload Settings")]
    private float holdDuration;

    [SerializeField]
    private List<Palier> stages = new List<Palier>();

    public static int CurrentScore {  get; private set; } = 0;
    public static int NbTaskFail { get; private set; } = 0;

    private float currentHoldTime = 0f;

    private bool loadingScene = false;

    private void Start()
    {
        foreach(Task t in SCO_Ref.AllTasks)
        {
            t.onTaskFinished += TaskFinished;
            t.onTaskFailed += TaskFailed;
        }
    }

    private void Update()
    {
        if (loadingScene)
            return;

        if (Input.GetKey(KeyCode.Keypad0))
        {
            currentHoldTime += Time.deltaTime;

            if(currentHoldTime >= holdDuration)
            {
                // Charger la scène
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                loadingScene = true;
            }
        }
        else if(Input.GetKeyUp(KeyCode.Keypad0))
        {
            currentHoldTime = 0f;
        }
    }

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
