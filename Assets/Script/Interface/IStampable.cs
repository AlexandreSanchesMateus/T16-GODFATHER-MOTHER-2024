using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStampable
{
    public abstract void SetRef(Task other);
    public abstract Task GetRef();
    public abstract bool Stamped();
}
