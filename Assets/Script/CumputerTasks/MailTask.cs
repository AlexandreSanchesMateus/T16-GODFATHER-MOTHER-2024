using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailTask : MonoBehaviour
{
    private List<Mail> _mails;
    private Mail _currentMail;

    [SerializeField]
    private GameObject mailPrefab;

    [SerializeField]
    private float backspaceFrk = 0.2f;
    [SerializeField]
    private int MaxStackableMail = 3;

    [SerializeField]
    private int minLetterRequested = 20;
    [SerializeField]
    private int maxLetterRequested = 120;

    public void CreateNewMail()
    {
        // Faire un mail
        if (_mails.Count >= MaxStackableMail)
            return;

        GameObject mail = Instantiate(mailPrefab,new Vector3(0, 0, 0), Quaternion.identity, transform);
        mail.transform.SetAsFirstSibling();

        // Ajouter à la liste
        _mails.Add(mail.GetComponent<Mail>());
    }

    public void SendNewKeystrok(string str)
    {
        // Ecrire dans le TextMeshPro
        _currentMail.White(str);
    }

    public void RemoveLastKeystrok()
    {
        // Effacer avec une fréquence

        // Actualiser le nombre de lettre
    }

    public void SendMail()
    {
        // Envoyer le mail
        // Vérifier taille !
    }
}
