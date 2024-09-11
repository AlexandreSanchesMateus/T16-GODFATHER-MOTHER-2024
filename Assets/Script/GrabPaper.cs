using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GrabPaper : MonoBehaviour
{

    [SerializeField]
    private float grabDuration = 2f;
    [SerializeField]
    LayerMask paperLayer;

    public GameObject Paper;
    public Transform CamGrabPoint;

    void Start()
    {
   
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, 5000, paperLayer))
        {
            if(hitInfo.collider != null)
            {
                Debug.Log("Hit Somthing : " + hitInfo.transform.name);
                if (Input.GetMouseButtonDown(0))
                    LeftDownCorner();
            }
        }


       /* if (Input.GetMouseButtonDown(0))
        {
            LeftDownCorner();
        }*/
    }

    public void LeftDownCorner()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(CamGrabPoint != null)
            {
                Paper.transform.SetParent(CamGrabPoint!.transform);
                Paper.transform.DOLocalMove(new Vector3(0, 0, 0), grabDuration);

            }else{
            return;
        }
    }
}
