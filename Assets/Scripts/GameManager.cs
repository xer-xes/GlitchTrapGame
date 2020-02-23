using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public Fade glitchFadedWallpaper;
    public GameObject playButtonsCanvas;
    public GameObject LSD;
    public GameObject Tongue;

    public GameObject backGround;

    bool lsdOnTongue = false;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        if(glitchFadedWallpaper.isFade == true)
        {
            playButtonsCanvas.gameObject.SetActive(false);
            LSD.gameObject.SetActive(true);
            Tongue.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.S))            // Temporary to skip the initial section
            FadeToBlack();
    }

    public void FadeToBlack()
    {
        float colorR = backGround.GetComponent<SpriteRenderer>().color.r;
        float colorG = backGround.GetComponent<SpriteRenderer>().color.g;
        float colorB = backGround.GetComponent<SpriteRenderer>().color.b;
        backGround.GetComponent<SpriteRenderer>().color = new Color(colorR, colorG, colorB,0);
        StartCoroutine("LoadLauncher");
    }

    private IEnumerator LoadLauncher()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Launcher");
    }
}
