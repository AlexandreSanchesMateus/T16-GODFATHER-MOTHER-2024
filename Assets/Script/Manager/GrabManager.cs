using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabManager : MonoBehaviour
{
    [SerializeField, BoxGroup("Init"), Required]
    private Camera cam;

    [SerializeField, BoxGroup("Grab Settings")]
    private LayerMask paperLayer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            CheckRay();
    }

    private void CheckRay()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 5000, paperLayer))
        {
            if (hitInfo.collider != null && hitInfo.transform.TryGetComponent<IInteractible>(out IInteractible interactible))
                interactible.Interact();
        }
    }
}
