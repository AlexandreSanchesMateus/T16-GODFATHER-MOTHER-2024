using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FaxTask : Task, IInteractible
{
    [SerializeField, BoxGroup("Init")]
    private Transform faxExit;
    [SerializeField, BoxGroup("Init")]
    private Transform grabPos;
    [SerializeField, BoxGroup("Init")]
    private Transform binPos;
    [SerializeField, BoxGroup("Init")]
    private PaperTask paperTask;
    [SerializeField, BoxGroup("Init")]
    private List<GameObject> faxPrefabs = new List<GameObject>();

    private Fax currentFax = null;
    private Fax faxInHand = null;

    [SerializeField, BoxGroup("Paper Settings")]
    private float faxReceivedFrequency = 10f;
    [SerializeField, BoxGroup("Paper Settings")]
    private float faxReceivedRandomness = 5f;

    [SerializeField, Foldout("Events")]
    private UnityEvent onFaxGrab;
    [SerializeField, Foldout("Events")]
    private UnityEvent OnFaxRelease;
    [SerializeField, Foldout("Events")]
    private UnityEvent OnFaxThrowAway;

    private float faxTimer;

    private void Start()
    {
        faxTimer = faxReceivedFrequency + Random.Range(-faxReceivedRandomness, faxReceivedRandomness);
    }

    private void Update()
    {
        if (faxTimer <= 0f)
        {
            faxTimer = faxReceivedFrequency + Random.Range(-faxReceivedRandomness, faxReceivedRandomness);
            // Create new fax
            if (!currentFax)
            {
                GameObject fax = Instantiate(faxPrefabs[Random.Range(0, faxPrefabs.Count)], faxExit.position, faxExit.rotation, null);
                fax.transform.DOMove(fax.transform.position + new Vector3(0.298f, 0, -0.068999f), 0.8f);

                currentFax = fax.GetComponent<Fax>();
                currentFax.SetRef(this);
                _onTaskRecived?.Invoke(this);
                HaveTask = true;
            }
            else
                _onTaskFailed?.Invoke(this);
        }
        else
            faxTimer -= Time.deltaTime;
    }

    public void Interact()
    {
        if (!currentFax || faxInHand)
            return;

        if (currentFax == null)
            HaveTask = false;

        onFaxGrab?.Invoke();

        // Récupérer le fax
        currentFax.transform.SetParent(grabPos);
        currentFax.transform.DOLocalMove(Vector3.zero, 0.4f);
        currentFax.transform.DOLocalRotate(Vector3.zero, 0.4f);
        faxInHand = currentFax;
        currentFax = null;
    }

    public void PutFaxOnDesk()
    {
        if (!faxInHand || paperTask.IsDeskUsed())
            return;

        OnFaxRelease?.Invoke();

        // Move
        faxInHand.DOKill();
        faxInHand.transform.SetParent(null);
        faxInHand.transform.DOMove(paperTask.DeskPos.position, 0.4f);
        faxInHand.transform.DORotate(new Vector3(0, -90, 0), 0.4f);
        paperTask.SetObjectOnDesk(faxInHand.gameObject);
        faxInHand = null;
    }

    public void PutFaxInBin()
    {
        if (!faxInHand)
            return;

        if (faxInHand.IsApprouved)
            _onTaskFailed?.Invoke(this);
        else
            _onTaskFinished?.Invoke(this);

        OnFaxThrowAway?.Invoke();

        faxInHand.transform.SetParent(null);
        GameObject fax = faxInHand.gameObject;
        Sequence throwAway = DOTween.Sequence();
        throwAway.Append(fax.transform.DOJump(binPos.position, 0.3f, 1, 0.2f));
        throwAway.AppendCallback(() => Destroy(fax));

        faxInHand = null;
    }
}
