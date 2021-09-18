using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QueenPiece : MonoBehaviour
{
    public float bopHeight = 1.0f;
    public float startHeight = 1.0f;
    public float time = 1.0f;
    public float rotateTime = 0.5f;

    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = transform.position;
        pos.y = startHeight;
        transform.position = pos;
        var seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveY(startHeight + bopHeight, time).SetEase(Ease.Linear));
        seq.Append(transform.DOLocalMoveY(startHeight, time).SetEase(Ease.Linear));
        seq.SetLoops(-1);
        seq.Play();

        var seq2 = DOTween.Sequence();
        var lastRotate = transform.DORotate(new Vector3(0, 360, 0), rotateTime).SetEase(Ease.Linear);
        lastRotate.onComplete += () => { transform.eulerAngles = Vector3.zero; };
        seq2.Append(transform.DORotate(new Vector3(0, 90, 0), rotateTime).SetEase(Ease.Linear));
        seq2.Append(transform.DORotate(new Vector3(0, 180, 0), rotateTime).SetEase(Ease.Linear));
        seq2.Append(transform.DORotate(new Vector3(0, 270, 0), rotateTime).SetEase(Ease.Linear));
        seq2.Append(lastRotate);
        seq2.SetLoops(-1);

        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            other.GetComponent<PlayerController>().SetQueen();
            Destroy(parent);
        }
    }
}
