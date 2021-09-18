using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public float fadeTime;
    public Image fadeImage;

    // Start is called before the first frame update
    void Start()
    {
        fadeImage.DOColor(new Color(0, 0, 0, 0), fadeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        fadeImage.DOColor(Color.black, fadeTime).onComplete +=
            () => { SceneManager.LoadScene(1); };
    }
}
