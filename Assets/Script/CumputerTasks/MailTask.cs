using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MailTask : MonoBehaviour
{
    private Queue<Mail> _mails;
    private Mail _currentMail;

    bool _canRemove = true;

    [SerializeField, BoxGroup("Init")]
    private GameObject mailPrefab;
    [SerializeField, BoxGroup("Init")]
    private GameObject mailNotification;
    [SerializeField, BoxGroup("Init")]
    private TextMeshProUGUI notifTxt;

    [SerializeField, BoxGroup("Settings")]
    private float backspaceFrk = 0.2f;
    [SerializeField, BoxGroup("Settings")]
    private int MaxStackableMail = 5;

    [SerializeField, BoxGroup("Settings")]
    private int minLetterRequested = 20;
    [SerializeField, BoxGroup("Settings")]
    private int maxLetterRequested = 120;

    [SerializeField, Foldout("Events")]
    private UnityEvent OnMailArrive;
    [SerializeField, Foldout("Events")]
    private UnityEvent OnMailSendGood;
    [SerializeField, Foldout("Events")]
    private UnityEvent OnMailSendBad;
    [SerializeField, Foldout("Events")]
    private UnityEvent OnWhiteMail;
    [SerializeField, Foldout("Events")]
    private UnityEvent OnRevertMail;


    public void CreateNewMail()
    {
        // Faire un mail
        if (_mails.Count >= MaxStackableMail)
            return;

        Mail mail = Instantiate(mailPrefab,new Vector3(0, 0, 0), Quaternion.identity, transform).GetComponent<Mail>();
        mail.InitMail(Random.Range(minLetterRequested, maxLetterRequested));
        mail.transform.SetAsFirstSibling();

        // Ajouter à la liste
        _mails.Enqueue(mail);

        // Notif
        mailNotification.SetActive(true);
        notifTxt.text = _mails.Count.ToString();

        OnMailArrive?.Invoke();
    }
    
    public void SendNewKeystrok(string str)
    {
        // Ecrire dans le TextMeshPro
        _currentMail.White(str);
        OnWhiteMail?.Invoke();
    }

    public void RemoveLastKeystrok()
    {
        // Effacer avec une fréquence
        if (_canRemove)
        {
            _currentMail.BackSpace();
            OnRevertMail?.Invoke();

            _canRemove = false;
            StartCoroutine("WaitToRemove");
        }
    }

    public void SendMail()
    {
        // Envoyer le mail
        // Vérifier taille !

        if (_mails.Dequeue().Send())
            OnMailSendGood?.Invoke();
        else
            OnMailSendBad?.Invoke();

        if(_mails.Count > 0)

        if (_mails.Count <= 0)
        {
            mailNotification.SetActive(false);
            _currentMail = null;
        }
        else
        {
            _currentMail = _mails.Peek();
            notifTxt.text = _mails.Count.ToString();
        }
    }

    IEnumerable WaitToRemove()
    {
        yield return new WaitForSeconds(backspaceFrk);
        _canRemove = true;
    }
}
