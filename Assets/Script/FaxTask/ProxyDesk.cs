using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyDesk : MonoBehaviour, IInteractible
{
    [SerializeField]
    private FaxTask faxTask;

    public void Interact()
    {
        faxTask.PutFaxOnDesk();
    }
}
