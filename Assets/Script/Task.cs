using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Task : MonoBehaviour
{
    [SerializeField]
    private CamRotationManager.ECamRotState taskLocation;
    public CamRotationManager.ECamRotState CamRotState { get {  return taskLocation; } }

    [SerializeField, Foldout("Events")] protected UnityEvent<Task> _onTaskRecived;
    public event UnityAction<Task> onTaskRecived { add => _onTaskRecived.AddListener(value); remove => _onTaskRecived.RemoveListener(value); }

    [SerializeField, Foldout("Events")] protected UnityEvent<Task> _onTaskFinished;
    public event UnityAction<Task> onTaskFinished { add => _onTaskFinished.AddListener(value); remove => _onTaskFinished.RemoveListener(value); }

    public bool HaveTask { get; protected set; }

    public bool IsActive { get; private set; } = false;

    public virtual void Activate() { IsActive = true; }
    public virtual void Deactivate() { IsActive = false; }
}
