using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatingHead : MonoBehaviour
{
    public float bopHeight = 1.0f;
    public float time = 1.0f;
    public float rotateTime = 1.0f;
    public float originalRotX;

    // Start is called before the first frame update
    void Start()
    {
        var seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveY(bopHeight, time).SetEase(Ease.Linear));
        seq.Append(transform.DOLocalMoveY(0, time).SetEase(Ease.Linear));
        seq.SetLoops(-1);
        seq.Play();

        var rotSeq = DOTween.Sequence();
        var rotFinish = transform.DOLocalRotate(new Vector3(originalRotX, 0, 360), rotateTime).SetEase(Ease.Linear);
        rotFinish.onComplete += () => { transform.eulerAngles = new Vector3(originalRotX, 0, 0); };
        rotSeq.Append(transform.DOLocalRotate(new Vector3(originalRotX, 0, 90), rotateTime).SetEase(Ease.Linear));
        rotSeq.Append(transform.DOLocalRotate(new Vector3(originalRotX, 0, 180), rotateTime).SetEase(Ease.Linear));
        rotSeq.Append(transform.DOLocalRotate(new Vector3(originalRotX, 0, 270), rotateTime).SetEase(Ease.Linear));
        rotSeq.Append(rotFinish);
        rotSeq.SetLoops(-1);
        rotSeq.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
