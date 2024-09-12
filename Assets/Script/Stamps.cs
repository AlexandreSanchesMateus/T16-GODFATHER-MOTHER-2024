using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamps : MonoBehaviour
{
    [SerializeField]
    private float grabDuration = 2f;
    private float stampDuration = 10f;
    [SerializeField]
    LayerMask paperLayer;

    public GameObject Paper;
    public Transform CamGrabPoint;

    [SerializeField]
    private Transform targetPosition;
    public Transform IsStampingHere;

    private bool isHoldingPaper = false;
    private bool isPlaced = false;
    private bool isReturn = false;
    private Rigidbody paperRigidbody;

    void Start()
    {

        if (Paper != null)
        {

            paperRigidbody = Paper.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {

        if (isPlaced)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 5000, paperLayer))
        {

            if (hitInfo.collider != null)
            {

                Debug.Log("Hit Something : " + hitInfo.transform.name);

                if (Input.GetMouseButtonDown(0))
                {

                    if (!isHoldingPaper)
                    {

                        LeftDownCorner();
                        isHoldingPaper = true;

                    }
                    else
                    {

                        PlacePaper();
                        IsStamps();
                        LeftDownCorner();
                    }
                }
            }
        }
    }

    public void LeftDownCorner()
    {

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (CamGrabPoint != null)
        {

            Paper.transform.SetParent(CamGrabPoint.transform);
            Paper.transform.DOLocalMove(Vector3.zero, grabDuration);

            if (paperRigidbody != null)
            {

                paperRigidbody.isKinematic = true;
            }

        }
        else
        {
            return;
        }
    }

    private void PlacePaper()
    {

        if (targetPosition != null)
        {

            Paper.transform.SetParent(null);
            Paper.transform.DOMove(targetPosition.position, grabDuration);

        }
        else
        {

            Debug.LogWarning("Target position pas mis !");
        }
    }

    void IsStamps()
    {
        if (IsStampingHere != null)
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(transform.DOMoveX(4, 1));
            mySequence.Append(transform.DORotate(new Vector3(0, 180, 0), 1));
            mySequence.PrependInterval(1);
            mySequence.Insert(0, transform.DOScale(new Vector3(3, 3, 3), mySequence.Duration()));
            Paper.transform.SetParent(null);
            Paper.transform.DOJump(IsStampingHere.position, 5, 2, stampDuration, false);
        }
    }

}