using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Mail : MonoBehaviour
{
    [SerializeField]
    private List<string> destinationNames = new List<string>();

    [SerializeField]
    private TextMeshProUGUI destinataireTxt;
    [SerializeField]
    private TextMeshProUGUI corpTxt;
    [SerializeField]
    private TextMeshProUGUI nbLetterTxt;

    private int targetLetterNb;

    public void InitMail(int nbLettersRequested)
    {
        if (destinationNames.Count > 0)
            destinataireTxt.text = "A " + destinationNames[Random.Range(0, destinationNames.Count)];

        targetLetterNb = nbLettersRequested;
        nbLetterTxt.text = "0/ " + targetLetterNb;
    }

    public void White(string str)
    {
        corpTxt.text += str;
        nbLetterTxt.text = corpTxt.text.Length + "/ " + targetLetterNb;
    }

    public bool Send()
    {
        Destroy(gameObject);
        return corpTxt.text.Length == targetLetterNb;
    }
}
