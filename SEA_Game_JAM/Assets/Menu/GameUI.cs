using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameUI : MonoBehaviour
{
    public float score = 0;
    public int finalScore = 0;
    public float scoreSpeed = 1000;
    const int maxScore = 999999999;
    public TextMeshProUGUI scoreTxt;

    public Image modeImage;
    public float modeRotateTime = 0.2f;

    public WaveGenerator waveGen;
    public Image fade;
    public float fadeTime;

    // Start is called before the first frame update
    void Start()
    {
        fade.DOColor(new Color(0, 0, 0, 0), fadeTime).onComplete +=
            ()=> { waveGen.gameObject.SetActive(true); } ;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        if (finalScore > maxScore) finalScore = maxScore;
        if (score < finalScore) score += scoreSpeed * Time.deltaTime;
        if (score > finalScore) score = finalScore;
        scoreTxt.text = ((int)score).ToString("000000000");
    }

    public void RotateMode(int _dir)
    {
        Vector3 newRot = modeImage.transform.eulerAngles;
        newRot.z -= _dir * 90;
        if (newRot.z >= 360) newRot.z -= 360;
        if (newRot.z <= 0) newRot.z += 360;
        modeImage.transform.DOLocalRotateQuaternion(Quaternion.Euler(newRot), modeRotateTime);
    }
}
