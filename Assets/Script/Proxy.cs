using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proxy : MonoBehaviour, IInteractible
{
    [SerializeField, Required]
    private PaperTask taskRef;

    public void Interact()
    {
        taskRef.PaperStamped();
    }
}
