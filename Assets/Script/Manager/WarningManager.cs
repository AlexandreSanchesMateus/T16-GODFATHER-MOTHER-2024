using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningManager : MonoBehaviour
{
    [SerializeField, BoxGroup("Init"), Required]
    private SCO_Tasks SCO_Ref;
    [SerializeField, BoxGroup("Init"), Required]
    private CamRotationManager camRotationManager;
    [SerializeField, BoxGroup("Init")]
    private GameObject leftWarningArrow;
    [SerializeField, BoxGroup("Init")]
    private GameObject rightWarningArrow;

    private Sequence LeftBlinkSequence;
    private Sequence RightBlinkSequence;

    void Start()
    {
        leftWarningArrow.SetActive(false);
        rightWarningArrow.SetActive(false);

        camRotationManager.onStateChange += UpdateWarning;

        foreach(Task t in SCO_Ref.AllTasks)
        {
            t.onTaskRecived += UpdateTaskRecived;        
        }
    }

    private void UpdateTaskRecived(Task other)
    {
        switch (camRotationManager.CamState)
        {
            case CamRotationManager.ECamRotState.LEFT:
                if(other.CamRotState != CamRotationManager.ECamRotState.LEFT)
                {
                    // Blink Right
                    BlinkRight();
                }
                break;

            case CamRotationManager.ECamRotState.MIDDLE:
                if (other.CamRotState == CamRotationManager.ECamRotState.LEFT)
                {
                    // Blink Left
                    BlinkLeft();
                }
                else if (other.CamRotState == CamRotationManager.ECamRotState.RIGHT)
                {
                    // Blink Right
                    BlinkRight();
                }

                break;

            case CamRotationManager.ECamRotState.RIGHT:
                if (other.CamRotState != CamRotationManager.ECamRotState.RIGHT)
                {
                    // Blink Left
                    BlinkLeft();
                }
                break;
        }
    }

    private void BlinkRight()
    {
        if (RightBlinkSequence != null)
        {
            if(RightBlinkSequence.IsPlaying())
                RightBlinkSequence.Restart();
            else
                RightBlinkSequence.Play();
        }
        else
        {
            RightBlinkSequence = DOTween.Sequence();
            RightBlinkSequence.AppendCallback(() => rightWarningArrow.SetActive(true));
            RightBlinkSequence.AppendInterval(0.2f);
            RightBlinkSequence.AppendCallback(() => rightWarningArrow.SetActive(false));
            RightBlinkSequence.AppendInterval(0.2f);
            RightBlinkSequence.AppendCallback(() => rightWarningArrow.SetActive(true));
            RightBlinkSequence.AppendInterval(0.2f);
            RightBlinkSequence.AppendCallback(() => rightWarningArrow.SetActive(false));
            RightBlinkSequence.AppendInterval(0.2f);
            RightBlinkSequence.AppendCallback(() => rightWarningArrow.SetActive(true));
        }
    }

    private void BlinkLeft()
    {
        if (LeftBlinkSequence != null)
            if (LeftBlinkSequence.IsPlaying())
                LeftBlinkSequence.Restart();
            else
                LeftBlinkSequence.Play();
        else
        {
            LeftBlinkSequence = DOTween.Sequence();
            LeftBlinkSequence.AppendCallback(() => leftWarningArrow.SetActive(true));
            LeftBlinkSequence.AppendInterval(0.2f);
            LeftBlinkSequence.AppendCallback(() => leftWarningArrow.SetActive(false));
            LeftBlinkSequence.AppendInterval(0.2f);
            LeftBlinkSequence.AppendCallback(() => leftWarningArrow.SetActive(true));
            LeftBlinkSequence.AppendInterval(0.2f);
            LeftBlinkSequence.AppendCallback(() => leftWarningArrow.SetActive(false));
            LeftBlinkSequence.AppendInterval(0.2f);
            LeftBlinkSequence.AppendCallback(() => leftWarningArrow.SetActive(true));
        }
    }

    private void UpdateWarning(CamRotationManager.ECamRotState state)
    {
        rightWarningArrow.SetActive(false);
        leftWarningArrow.SetActive(false);

        bool lWarning = false;
        bool rWarning = false;

        switch (state)
        {
            case CamRotationManager.ECamRotState.LEFT:
                foreach (Task t in SCO_Ref.MiddleTasks)
                {
                    if (t.HaveTask)
                    {
                        rWarning = true;
                        break;
                    }
                }

                if (!rWarning)
                {
                    foreach (Task t in SCO_Ref.RightTasks)
                    {
                        if (t.HaveTask)
                        {
                            rWarning = true;
                            break;
                        }
                    }
                }

                if (rWarning)
                    rightWarningArrow.SetActive(true);
                break;

            case CamRotationManager.ECamRotState.MIDDLE:

                foreach (Task t in SCO_Ref.LeftTasks)
                {
                    if (t.HaveTask)
                    {
                        leftWarningArrow.SetActive(true);
                        break;
                    }
                }

                foreach (Task t in SCO_Ref.RightTasks)
                {
                    if (t.HaveTask)
                    {
                        rightWarningArrow.SetActive(true);
                        break;
                    }
                }
                break;

            case CamRotationManager.ECamRotState.RIGHT:
                foreach (Task t in SCO_Ref.MiddleTasks)
                {
                    if (t.HaveTask)
                    {
                        lWarning = true;
                        break;
                    }
                }

                if (!lWarning)
                {
                    foreach (Task t in SCO_Ref.LeftTasks)
                    {
                        if (t.HaveTask)
                        {
                            lWarning = true;
                            break;
                        }
                    }
                }

                if (lWarning)
                    leftWarningArrow.SetActive(true);
                break;
        }
    }

    private void OnDestroy()
    {
        camRotationManager.onStateChange -= UpdateWarning;
        foreach (Task t in SCO_Ref.AllTasks)
        {
            t.onTaskRecived -= UpdateTaskRecived;
        }
    }
}
