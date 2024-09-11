using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cumputer : Task
{
    [SerializeField, BoxGroup("Init")]
    private Transform displayTrs;
    [SerializeField, BoxGroup("Init")]
    private MailTask mailScript;
    [SerializeField, BoxGroup("Init")]
    private PopupTask popupScript;
    // private GameObject Prefab;

    void Update()
    {
        if (!IsActive)
            return;

        if (Input.anyKeyDown)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                // Remove Popup
            }
            else if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                // Enter for mail
                Debug.Log("Enter");
                mailScript.SendMail();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Back");
                mailScript.RemoveLastKeystrok();
            }
            else
            {
                // Evoyer au mail
                string keyStr = Input.inputString;
                mailScript.SendNewKeystrok(keyStr);
            }
        }
    }

    [Button]
    public override void Activate()
    {
        base.Activate();
        //throw new System.NotImplementedException();
    }

    [Button]
    public override void Deactivate()
    {
        base.Deactivate();
        //throw new System.NotImplementedException();
    }
}
