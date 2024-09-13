using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cumputer : Task
{
    [SerializeField, BoxGroup("Init")]
    private MailTask mailScript;
    [SerializeField, BoxGroup("Init")]
    private PopupTask popupScript;

    [SerializeField, BoxGroup("Mail Settings")]
    private float mailReceivedFrequency = 10f;
    [SerializeField, BoxGroup("Mail Settings")]
    private float mailReceivedRandomness = 5f;

    [SerializeField, BoxGroup("Popup Settings")]
    private float popupFrequency = 20f;
    [SerializeField, BoxGroup("Popup Settings")]
    private float popupRandomness = 5f;

    float mailTimer;
    float popupTimer;

    private void Start()
    {
        // start timers
        mailTimer = mailReceivedFrequency + Random.Range(-mailReceivedRandomness, mailReceivedRandomness);        
        popupTimer = popupFrequency + Random.Range(-popupRandomness, popupRandomness);
    }

    void Update()
    {
        // Timers
        if(mailTimer <= 0f)
        {
            mailTimer = mailReceivedFrequency + Random.Range(-mailReceivedRandomness, mailReceivedRandomness);
            if (mailScript.CreateNewMail())
            {
                _onTaskRecived?.Invoke(this);
                HaveTask = true;
            }
            else
                _onTaskFailed?.Invoke(this);
        }
        else
            mailTimer -= Time.deltaTime;

        if(popupTimer <= 0f)
        {
            popupTimer = popupFrequency + Random.Range(-popupRandomness, popupRandomness);
            popupScript.CreateNewPopups();
        }
        else
            popupTimer -= Time.deltaTime;


        // Inputs
        if (!IsActive)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Remove Popup
            // Debug.Log("Remove Popup");
            popupScript.CloseOnePopup();
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // Enter for mail
            // Debug.Log("Enter");
            if (mailScript.SendMail() && mailScript.MailNumber <= 0)
            {
                _onTaskFinished?.Invoke(this);
                HaveTask = false;
            }
            else
                _onTaskFailed?.Invoke(this);
        }
        else if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.Backspace))
            {
                // Debug.Log("Back");
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
}
