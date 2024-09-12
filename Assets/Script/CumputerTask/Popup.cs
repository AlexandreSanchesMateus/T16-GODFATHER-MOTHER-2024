using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Popup : MonoBehaviour
{
    // Random Visual ?
    [SerializeField]
    private TextMeshProUGUI m_TextMeshPro;

    [SerializeField]
    private List<string> errorList = new List<string>();

    private void Awake()
    {
        m_TextMeshPro.text = errorList[Random.Range(0, errorList.Count)];
    }

    public void ClosePopup()
    {
        // Animation ?
        Sequence clodeSequence = DOTween.Sequence();
        clodeSequence.Append(transform.DOScale(0, 0.12f).SetEase(Ease.InBack));
        clodeSequence.AppendCallback(() => Destroy(gameObject));
    }
}
