using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
