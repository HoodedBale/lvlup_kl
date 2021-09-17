using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatingHead : MonoBehaviour
{
    public float bopHeight = 1.0f;
    public float time = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        var seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMoveY(bopHeight, time).SetEase(Ease.Linear));
        seq.Append(transform.DOLocalMoveY(0, time).SetEase(Ease.Linear));
        seq.SetLoops(-1);
        seq.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
