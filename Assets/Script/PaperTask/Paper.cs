using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour, IStampable
{
    private Task taskRef;

    public void Grab()
    {
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<BoxCollider>());
    }

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
        return true;
    }
}
