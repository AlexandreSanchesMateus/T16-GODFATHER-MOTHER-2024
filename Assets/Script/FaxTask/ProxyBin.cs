using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyBin : MonoBehaviour, IInteractible
{
    [SerializeField, Required]
    private FaxTask faxTask;

    public void Interact()
    {
        faxTask.PutFaxInBin();
    }
}
