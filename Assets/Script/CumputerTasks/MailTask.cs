using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MailTask : MonoBehaviour
{
    private Queue<Mail> _mails = new Queue<Mail>();
    private Mail _currentMail = null;

    bool _canRemove = true;
    public int MailNumber { get {  return _mails.Count; } }

    [SerializeField, BoxGroup("Init")]
    private GameObject mailPrefab;
    [SerializeField, BoxGroup("Init")]
    private GameObject mailNotification;
    [SerializeField, BoxGroup("Init")]
    private TextMeshProUGUI notifTxt;

    [SerializeField, BoxGroup("Settings")]
    private float backspaceFrk = 0.05f;
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


    public bool CreateNewMail()
    {
        // Faire un mail
        if (_mails.Count >= MaxStackableMail)
            return false;

        Mail mail = Instantiate(mailPrefab, transform).GetComponent<Mail>();
        mail.InitMail(Random.Range(minLetterRequested, maxLetterRequested));
        mail.transform.SetAsFirstSibling();
        mail.transform.localPosition = Vector3.zero;

        // Ajouter à la liste
        _mails.Enqueue(mail);

        if (!_currentMail)
        {
            _currentMail = mail;
            transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.6f);
        }

        // Notif
        mailNotification.SetActive(true);
        notifTxt.text = _mails.Count.ToString();

        OnMailArrive?.Invoke();
        return true;
    }
    
    public void SendNewKeystrok(string str)
    {
        if (_currentMail == null)
            return;

        // Ecrire dans le TextMeshPro
        _currentMail.White(str);
        OnWhiteMail?.Invoke();
    }

    public void RemoveLastKeystrok()
    {
        if (_currentMail == null)
            return;

        // Effacer avec une fréquence
        if (_canRemove)
        {
            Debug.Log("BITE");

            _currentMail.BackSpace();

            _canRemove = false;
            StartCoroutine(WaitToRemove());

            OnRevertMail?.Invoke();
        }
    }

    public bool SendMail()
    {
        // Envoyer le mail
        // Vérifier taille !

        if(_currentMail == null)
            return false;

        if (_currentMail.Send())
        {
            OnMailSendGood?.Invoke();
            _mails.Dequeue();

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
            return true;
        }
        else
            OnMailSendBad?.Invoke();

        return false;
    }

    IEnumerator WaitToRemove()
    {
        yield return new WaitForSeconds(backspaceFrk);
        _canRemove = true;
    }
}
