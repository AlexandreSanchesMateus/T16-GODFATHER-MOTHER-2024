using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    public bool IsActive { get; private set; } = false;

    public virtual void Activate() { IsActive = true; }
    public virtual void Deactivate() { IsActive = false; }
}
