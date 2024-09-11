using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mail : MonoBehaviour
{
    [SerializeField]
    private List<string> destinationNames = new List<string>();
    [SerializeField]
    private List<string> subjectNames = new List<string>();

    [SerializeField]
    private TextMeshProUGUI destinataireTxt;
    [SerializeField]
    private TextMeshProUGUI subjectTxt;
    [SerializeField]
    private TextMeshProUGUI corpTxt;
    [SerializeField]
    private TextMeshProUGUI nbLetterTxt;

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
        corpTxt.text = corpTxt.text.Remove(corpTxt.text.Length - 1);
        nbLetterTxt.text = corpTxt.text.Length + "/ " + targetLetterNb;
    }

    public bool Send()
    {
        Destroy(gameObject);
        return corpTxt.text.Length == targetLetterNb;
    }
}
