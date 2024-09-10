using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using NaughtyAttributes;

public class CamRotationManager : MonoBehaviour
{
    private CamRotation mCurrentState = CamRotation.MIDDLE;
    private int mcurrentRotation = 0;

    [SerializeField]
    private float turnDuration = 1f;

    [SerializeField, Foldout("Events")]
    private UnityEvent OnLeftState;
    [SerializeField, Foldout("Events")]
    private UnityEvent OnMiddleState;
    [SerializeField, Foldout("Events")]
    private UnityEvent OnRightState;

    enum CamRotation
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
        if (mCurrentState == CamRotation.LEFT)
            return;

        mcurrentRotation = mcurrentRotation - 90;
        --mCurrentState;
        transform.DORotate(new Vector3(0, mcurrentRotation, 0), turnDuration);
        TriggerEvent();
    }

    public void RotateCamRight()
    {
        if (mCurrentState == CamRotation.RIGHT)
            return;

        mcurrentRotation = mcurrentRotation + 90;
        ++mCurrentState;
        transform.DORotate(new Vector3(0, mcurrentRotation, 0), turnDuration);
        TriggerEvent();
    }

    private void TriggerEvent()
    {
        switch (mCurrentState)
        { 
            case CamRotation.LEFT:
                OnLeftState.Invoke();
                break;
            case CamRotation.MIDDLE:
                OnMiddleState.Invoke();
                break;
            case CamRotation.RIGHT:
                OnRightState.Invoke();
                break;
        }
    }
}
