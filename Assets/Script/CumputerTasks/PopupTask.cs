using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopupTask : MonoBehaviour
{
    // List popup
    List<Popup> _popups = new List<Popup>();

    [SerializeField, BoxGroup("Init")]
    private GameObject popupPrefab;

    [SerializeField, BoxGroup("Settings")]
    private int maxStackblePopups = 9;
    [SerializeField, BoxGroup("Settings")]
    private int nbPopupToAppear = 5;
    [SerializeField, BoxGroup("Settings")]
    private int randomness = 4;
    [SerializeField, BoxGroup("Settings")]
    private float minPopupInterval = 0.1f;
    [SerializeField, BoxGroup("Settings")]
    private float maxPopupInterval = 0.5f;
    [SerializeField, BoxGroup("Settings")]
    private float spawnRadius = 10f;

    [SerializeField, Foldout("Events")]
    private UnityEvent OnPopupAppear;
    [SerializeField, Foldout("Events")]
    private UnityEvent OnPopupClose;

    public bool CreateNewPopups()
    {
        if (popupPrefab == null)
            return false;

        // Create
        int nb = nbPopupToAppear + Random.Range(-randomness, randomness);
        nb = Mathf.Min(maxStackblePopups - _popups.Count, nb);

        if (nb == 0)
            return false;

        StopAllCoroutines();
        StartCoroutine(SpawnPopup(nb));
        return true;
    }

    public void CloseOnePopup()
    {
        // Random on list
        if(_popups.Count > 0)
        {
            Popup popup = _popups[Random.Range(0, _popups.Count)];
            popup.ClosePopup();
            _popups.Remove(popup);
            OnPopupClose?.Invoke();
        }
    }

    private IEnumerator SpawnPopup(int nb)
    {
        for (int i = 0; i < nb; i++)
        {
            GameObject instance = Instantiate(popupPrefab, this.transform);
            instance.transform.localPosition = new Vector3(Random.Range(-spawnRadius, spawnRadius), Random.Range(-spawnRadius, spawnRadius), 0);
            instance.transform.DOPunchScale(new Vector3(0.15f, 0.15f, 0), 0.3f);
            _popups.Add(instance.GetComponent<Popup>());

            OnPopupAppear?.Invoke();
            yield return new WaitForSeconds(Random.Range(minPopupInterval, maxPopupInterval));
        }
    }
}
