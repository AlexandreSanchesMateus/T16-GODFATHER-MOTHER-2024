using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class Mail : MonoBehaviour
{
    [SerializeField, BoxGroup("Init")]
    private TextMeshProUGUI destinataireTxt;
    [SerializeField, BoxGroup("Init")]
    private TextMeshProUGUI subjectTxt;
    [SerializeField, BoxGroup("Init")]
    private TextMeshProUGUI corpTxt;
    [SerializeField, BoxGroup("Init")]
    private TextMeshProUGUI nbLetterTxt;

    [SerializeField, Foldout("Déco")]
    private List<string> destinationNames = new List<string>();
    [SerializeField, Foldout("Déco")]
    private List<string> subjectNames = new List<string>();

    private int targetLetterNb;

    public void InitMail(int nbLettersRequested)
    {
        if (destinationNames.Count > 0)
            destinataireTxt.text = destinationNames[Random.Range(0, destinationNames.Count)];

        if (subjectNames.Count > 0)
            subjectTxt.text = subjectNames[Random.Range(0, subjectNames.Count)];

        targetLetterNb = nbLettersRequested;
        nbLetterTxt.text = "0/ " + targetLetterNb;
    }

    public void White(string str)
    {
        corpTxt.text += str;
        nbLetterTxt.text = corpTxt.text.Length + "/ " + targetLetterNb;
    }

    public void BackSpace() 
    {
        if (corpTxt.text.Length > 0)
        {
            corpTxt.text = corpTxt.text.Remove(corpTxt.text.Length - 1);
            nbLetterTxt.text = corpTxt.text.Length + "/ " + targetLetterNb;
        }
    }

    public bool Send()
    {
        bool valid = corpTxt.text.Length == targetLetterNb;

        if (valid)
        {
            Sequence validSequence = DOTween.Sequence();
            validSequence.Append(transform.DOLocalMoveY(1200, 0.45f).SetEase(Ease.InBack));
            validSequence.AppendCallback(() => Destroy(gameObject));
        }
        else
        {
            transform.parent.DOKill();
            transform.parent.transform.localPosition = Vector3.zero;
            transform.parent.DOPunchPosition(new Vector3(200, 0, 0), 0.6f);
        }

        return valid;
    }
}
