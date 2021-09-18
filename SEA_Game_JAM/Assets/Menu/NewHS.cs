using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewHS : MonoBehaviour
{
    public Vector3 enlarge;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(enlarge, time).SetEase(Ease.Linear));
        seq.Append(transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), time).SetEase(Ease.Linear));
        seq.SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
