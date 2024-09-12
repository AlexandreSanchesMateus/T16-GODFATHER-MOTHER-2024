using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash_action : MonoBehaviour
{
    [SerializeField]
    private float grabDuration = 2f;
    [SerializeField]
    LayerMask paperLayer;

    public GameObject Paper;
    public Transform CamGrabPoint;

    [SerializeField]
    private Transform targetPosition;

    private bool isHoldingPaper = false;
    private bool isPlaced = false;
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
                        isHoldingPaper = false;
                        isPlaced = true;
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
}