using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Task : MonoBehaviour
{
    [SerializeField, BoxGroup("General Settings")]
    private bool ActiveOnStart = false;

    [SerializeField, BoxGroup("General Settings")]
    private CamRotationManager.ECamRotState taskLocation;
    public CamRotationManager.ECamRotState CamRotState { get {  return taskLocation; } }

    [SerializeField, BoxGroup("General Settings")]
    private int taskWorth = 10;
    public int TaskWorth { get { return taskWorth; } }

    [SerializeField, Foldout("Events")] protected UnityEvent<Task> _onTaskRecived;
    public event UnityAction<Task> onTaskRecived { add => _onTaskRecived.AddListener(value); remove => _onTaskRecived.RemoveListener(value); }
    [SerializeField, Foldout("Events")] protected UnityEvent<Task> _onTaskFinished;
    public event UnityAction<Task> onTaskFinished { add => _onTaskFinished.AddListener(value); remove => _onTaskFinished.RemoveListener(value); }
    [SerializeField, Foldout("Events")] protected UnityEvent<Task> _onTaskFailed;
    public event UnityAction<Task> onTaskFailed { add => _onTaskFailed.AddListener(value); remove => _onTaskFailed.RemoveListener(value); }

    public bool HaveTask { get; protected set; }
    public bool IsActive { get; private set; } = false;

    private void Awake()
    {
        IsActive = ActiveOnStart;
    }

    [Button]
    public virtual void Activate() { IsActive = true; }
    [Button]
    public virtual void Deactivate() { IsActive = false; }
}
