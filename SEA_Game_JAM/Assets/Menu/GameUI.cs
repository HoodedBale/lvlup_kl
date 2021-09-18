using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    public GameObject loseScreen;
    public TextMeshProUGUI loseTxt;
    public GameObject newHS;
    public Image loseFade;

    Dictionary<int, string> m_loseMessages = new Dictionary<int, string>();

    // Start is called before the first frame update
    void Start()
    {
        fade.DOColor(new Color(0, 0, 0, 0), fadeTime).onComplete +=
            () => { waveGen.gameObject.SetActive(true); };

        m_loseMessages.Add(0, "Try Harder!");
        m_loseMessages.Add(2500, "Keep Going!");
        m_loseMessages.Add(10000, "Nice... I Guess?");
        m_loseMessages.Add(25000, "Alright!");
        m_loseMessages.Add(50000, "SICK!");
        m_loseMessages.Add(100000, "GOD!!!");
        m_loseMessages.Add(1000000, "What the heck are you?");
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        if (finalScore > maxScore) finalScore = maxScore;
        if (score < finalScore) score += (finalScore - score) * scoreSpeed * Time.deltaTime;
        if (score >= finalScore - 5) score = finalScore;
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

    public void ShowLoseScreen()
    {
        StartCoroutine(LoseScreenRoutine());

        int highScore = PlayerPrefs.GetInt("highscore", 0);
        if (finalScore > highScore)
        {
            PlayerPrefs.SetInt("highscore", finalScore);
            newHS.SetActive(true);
        }

        int bestMessage = 0;
        string loseMessage = "";

        foreach(var msg in m_loseMessages)
        {
            if(bestMessage < msg.Key && finalScore > msg.Key)
            {
                bestMessage = msg.Key;
                loseMessage = msg.Value;
            }
        }

        loseMessage += "\n\nScore: " + finalScore.ToString("000000000");
        loseTxt.text = loseMessage;

    }

    IEnumerator LoseScreenRoutine()
    {
        yield return new WaitForSeconds(2.0f);

        Sequence seq = DOTween.Sequence();
        seq.Append(loseFade.DOColor(Color.black, 1.0f));
        seq.Append(loseScreen.transform.DOScaleY(1, 0.5f));
        seq.Append(loseScreen.transform.DOScaleX(1, 0.5f));
    }

    public void LoadScene(int _id)
    {
        fade.DOColor(Color.black, 1.0f).onComplete +=
            () => { SceneManager.LoadScene(_id); };
    }
}
