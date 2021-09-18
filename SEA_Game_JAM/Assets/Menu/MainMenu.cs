using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public float fadeTime;
    public Image fadeImage;

    public TextMeshProUGUI highscore;

    public GameObject musicplayer;

    // Start is called before the first frame update
    void Start()
    {
        fadeImage.DOColor(new Color(0, 0, 0, 0), fadeTime);
        highscore.text = "HI: " + PlayerPrefs.GetInt("highscore", 0).ToString("000000000");

        DontDestroyOnLoad(musicplayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        GetComponent<AudioSource>().Play();
        fadeImage.DOColor(Color.black, fadeTime).onComplete +=
            () => { SceneManager.LoadScene(1); };
    }

    public void QuitGame()
    {
        GetComponent<AudioSource>().Play();
        fadeImage.DOColor(Color.black, fadeTime).onComplete +=
            () => { Application.Quit(); };
    }
}
