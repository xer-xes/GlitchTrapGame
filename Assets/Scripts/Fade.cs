using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image glitchFade;
    public bool isFade = false;

    void Start()
    {
        glitchFade.canvasRenderer.SetAlpha(1.0f);
    }

    public void FadeOut()
    {
        glitchFade.CrossFadeAlpha(0.0f, 3, false);
        isFade = true;
    }
}
