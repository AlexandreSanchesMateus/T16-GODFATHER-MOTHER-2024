using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Popup : MonoBehaviour
{
    // Random Visual ?

    public void ClosePopup()
    {
        // Animation ?
        Sequence clodeSequence = DOTween.Sequence();
        clodeSequence.Append(transform.DOScale(0, 0.12f).SetEase(Ease.InBack));
        clodeSequence.AppendCallback(() => Destroy(gameObject));
    }
}
