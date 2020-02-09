﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Dialog : MonoBehaviour
{
    public Button playButton;
    public Button onlineButton;
    public Button exitButton;

    public TextMeshProUGUI dialogueText;

    public string[] startSentence;
    public string[] playSentences;
    public string[] onlineSentences;
    public string[] exitSentences;

    public float typingSpeed;
    public float sentenceDelay;

    [Header("Camera Variables")]
    public GameObject camera;
    public float shakeDuration = 1;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;
    Vector3 cameraOriginalPosition;
    public bool cameraShake;

    private void OnEnable()
    {
        cameraOriginalPosition = camera.transform.localPosition;
    }

    private void Start()
    {
        StartCoroutine(OnPlayType(startSentence));
    }

    private void Update()
    {
        if(cameraShake)
        {
            CameraShake();
        }
    }

    //------------------------------
    public void OnClickPlayButton()
    {
        cameraShake = true;
        StartCoroutine(OnPlayType(playSentences));
        playButton.gameObject.SetActive(false);
    }

    //--------------------------------
    public void OnClickOnlineButton()
    {
        cameraShake = true;
        StartCoroutine(OnPlayType(onlineSentences));
        onlineButton.gameObject.SetActive(false);
    }

    //------------------------------
    public void OnClickExitButton()
    {
        cameraShake = true;
        StartCoroutine(OnPlayType(exitSentences));
        exitButton.gameObject.SetActive(false);
    }

    //-----------------------------------------
    IEnumerator OnPlayType(string[] sentences)
    {
        dialogueText.text = "";

        for (int i = 0; i < sentences.Length; i++)
        {
            foreach (char letter in sentences[i].ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            yield return new WaitForSeconds(sentenceDelay);
            if (i < sentences.Length - 1)
            {
                dialogueText.text = "";
            }
        }
    }

    void CameraShake()
    {
        if (shakeDuration > 0)
        {
            camera.transform.localPosition = cameraOriginalPosition + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            cameraShake = false;
            shakeDuration = 1f;
            camera.transform.localPosition = cameraOriginalPosition;
        }
    }
}
