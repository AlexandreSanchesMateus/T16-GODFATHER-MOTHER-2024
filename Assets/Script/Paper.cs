using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public void Grab()
    {
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<BoxCollider>());
    }
}
