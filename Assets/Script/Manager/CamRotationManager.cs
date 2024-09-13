using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using NaughtyAttributes;

public class CamRotationManager : MonoBehaviour
{
    private ECamRotState mCurrentState = ECamRotState.MIDDLE;
    public ECamRotState CamState { get {  return mCurrentState; } }
    private ECamRotState mLastState = ECamRotState.MIDDLE;
    private int mcurrentRotation = 0;

    [SerializeField]
    private SCO_Tasks SCO_Ref;
    [SerializeField]
    private float turnDuration = 1f;

    [SerializeField, Foldout("Events")]
    private UnityEvent<ECamRotState> OnStateChange;
    public event UnityAction<ECamRotState> onStateChange { add => OnStateChange.AddListener(value); remove => OnStateChange.RemoveListener(value); }

    [SerializeField, Foldout("Events")]
    private UnityEvent OnLeftState;
    [SerializeField, Foldout("Events")]
    private UnityEvent OnMiddleState;
    [SerializeField, Foldout("Events")]
    private UnityEvent OnRightState;

    public bool CanTriggerEvents { get; set; } = true;

    public enum ECamRotState
    {
        LEFT = 0,
        MIDDLE = 1,
        RIGHT = 2
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            RotateCamLeft();
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            RotateCamRight();
        }
    }

    public void RotateCamLeft()
    {
        if (mCurrentState == ECamRotState.LEFT)
            return;

        mcurrentRotation = mcurrentRotation - 90;
        --mCurrentState;
        transform.DORotate(new Vector3(0, mcurrentRotation, 0), turnDuration);

        if(CanTriggerEvents)
            TriggerEvents();
    }

    public void RotateCamRight()
    {
        if (mCurrentState == ECamRotState.RIGHT)
            return;

        mcurrentRotation = mcurrentRotation + 90;
        ++mCurrentState;
        transform.DORotate(new Vector3(0, mcurrentRotation, 0), turnDuration);

        if(CanTriggerEvents)
            TriggerEvents();
    }

    private void TriggerEvents()
    {
        // Désactiver
        PanelAction(mLastState, false);
        // Activer
        PanelAction(mCurrentState, true);

        // ReInit
        mLastState = mCurrentState;

        OnStateChange?.Invoke(mCurrentState);

        // Unity Events
        switch (mCurrentState)
        { 
            case ECamRotState.LEFT:
                OnLeftState.Invoke();
                break;
            case ECamRotState.MIDDLE:
                OnMiddleState.Invoke();
                break;
            case ECamRotState.RIGHT:
                OnRightState.Invoke();
                break;
        }
    }

    // Action on Panel
    private void PanelAction(ECamRotState rot, bool active)
    {
        if (!SCO_Ref)
        {
            Debug.LogError("Aucune référence à SCO_Tasks.");
            return;
        }

        switch (rot)
        {
            case ECamRotState.LEFT:
                TaskAction(SCO_Ref.LeftTasks, active);
                break;
            case ECamRotState.MIDDLE:
                TaskAction(SCO_Ref.MiddleTasks, active);
                break;
            case ECamRotState.RIGHT:
                TaskAction(SCO_Ref.RightTasks, active);
                break;
        }
    }

    // Action on tasks
    private void TaskAction(List<Task> list, bool active)
    {
        foreach (Task t in list)
        {
            if (active)
                t.Activate();
            else
                t.Deactivate();
        }
    }
}
