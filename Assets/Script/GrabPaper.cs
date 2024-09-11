using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPaper : MonoBehaviour
{

    [SerializeField]
    private float grabDuration = 2f;
    [SerializeField]
    LayerMask paperLayer;

    public GameObject Paper;
    public GameObject CamGrabPoint;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawRay(worldPos, new Vector3(0, 0, 1) * 50000, Color.red);

        if (Input.GetMouseButtonDown(0))
        {
            LeftDownCorner();
        }
    }

    public void LeftDownCorner()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Debug.DrawRay(worldPos, new Vector3(0, 0, 1) * 50000, Color.red);
        if (Physics.Raycast(worldPos, Camera.main.transform.rotation.eulerAngles, out RaycastHit hit, 50000, paperLayer)){

            Debug.Log("hello");

            /*if(CamGrabPoint != null)
            {
                Paper.transform.SetParent(CamGrabPoint!.transform);
                Paper.transform.DOLocalMove(new Vector3(0, 0, 0), grabDuration);
            }*/
        }else{
            return;
        }
    }
}
