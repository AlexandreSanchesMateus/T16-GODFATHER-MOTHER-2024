using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fax : MonoBehaviour, IStampable
{
    [SerializeField]
    private bool isApprouved;

    public bool IsApprouved {  get { return isApprouved; } }

    private Task taskRef;

    public void SetRef(Task other)
    {
        taskRef = other;
    }

    public Task GetRef()
    {
        return taskRef;
    }

    public bool Stamped()
    {
        return isApprouved;
    }
}
