using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTask : MonoBehaviour
{
    // List popup
    List<Popup> _popups;

    [SerializeField]
    private int maxStackblePopups = 9;
    [SerializeField]
    private int randomness = 4;

    [SerializeField]
    private GameObject popupPrefab;

    public void CreateNewPopups()
    {
        if (popupPrefab == null)
            return;

        // Create
        int nb = maxStackblePopups - _popups.Count + Random.Range(-randomness, randomness);
        for (int i = 0; i < nb; i++)
        {
            // TODO PLACER LE POPUP SUR L'ECRAN

            GameObject instance = Instantiate(popupPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
            _popups.Add(instance.GetComponent<Popup>());
        }
    }

    public void CloseOnePopup()
    {
        // Random on list
        if(_popups.Count > 0)
        {
            Popup popup = _popups[Random.Range(0, _popups.Count)];
            popup.ClosePopup();
            _popups.Remove(popup);
        }
    }
}
