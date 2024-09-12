using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperTask : Task, IInteractible
{
    Stack<Paper> _papers = new Stack<Paper>();
    Paper currentPaper = null;

    [SerializeField, BoxGroup("Init")]
    private GameObject paperPrefab;
    [SerializeField, BoxGroup("Init")]
    private Transform deskPos;
    [SerializeField, BoxGroup("Init")]
    private Transform folderTrail;
    [SerializeField, BoxGroup("Init")]
    private GameObject stampObject;

    [SerializeField, BoxGroup("Paper Settings")]
    private float paperReceivedFrequency = 10f;
    [SerializeField, BoxGroup("Paper Settings")]
    private float paperReceivedRandomness = 5f;
    [SerializeField, BoxGroup("Paper Settings")]
    private int maxStackablePapaer = 15;

    private float paperTimer;

    private void Start()
    {
        paperTimer = paperReceivedFrequency + Random.Range(-paperReceivedRandomness, paperReceivedRandomness);
    }

    private void Update()
    {
        if (paperTimer <= 0f)
        {
            paperTimer = paperReceivedFrequency + Random.Range(-paperReceivedRandomness, paperReceivedRandomness);
            // Create new paper
            if (_papers.Count < maxStackablePapaer)
            {
                // Crer
                Paper paper = Instantiate(paperPrefab, transform.position + new Vector3(0, 0.6f, 0), Quaternion.Euler(new Vector3(0, Random.Range(-20, 20), 0)), null).GetComponent<Paper>();
                _papers.Push(paper);
                _onTaskRecived?.Invoke(this);
                HaveTask = true;
            }
        }
        else
            paperTimer -= Time.deltaTime;
    }

    public void Interact()
    {
        // Prendre le premier feuille
        if (_papers.Count <= 0 || currentPaper)
            return;

        // Mettre sur le coté
        Paper paper = _papers.Pop();
        paper.Grab();
        paper.transform.DOJump(deskPos.position, 0.7f, 1, 0.8f);
        currentPaper = paper;
    }

    public void PaperStamped()
    {
        if(!currentPaper)
            return;

        GameObject feuille = currentPaper.gameObject;
        currentPaper = null;

        // Move paper to stach
        Sequence stamped = DOTween.Sequence();
        stamped.Append(stampObject.transform.DOJump(deskPos.position, 0.3f, 1, 0.2f));
        stamped.Append(stampObject.transform.DOLocalJump(Vector3.zero, 0.07f, 1, 0.2f));
        stamped.Append(feuille.transform.DOJump(folderTrail.position, 0.2f, 1, 0.5f));
        stamped.AppendCallback(() => Destroy(feuille));
    }
}
