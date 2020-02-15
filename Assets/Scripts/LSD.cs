using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LSD : MonoBehaviour
{
    private bool isBeingWrongHeld = false;
    public bool isBeingCorrectHeld = false;

    Vector3 defaultPosition;
    Vector3 targetPosition;

    Vector3 mousePosition;
    Vector3 offset = new Vector3(1.5f, -7, 0);

    float deltaX, deltaY = 0;

    bool isLocked;
    void Start()
    {        
        defaultPosition = gameObject.transform.localPosition;
        targetPosition = GameManager.Instance.Tongue.transform.GetChild(0).transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isBeingWrongHeld = true;
        }

        if(Input.GetMouseButtonDown(1))
        {
            isBeingCorrectHeld = true;
        }

        if (isBeingWrongHeld || isBeingCorrectHeld)
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition + offset);
            transform.position = new Vector3(mousePosition.x, mousePosition.y);
        }

        if(Input.GetMouseButtonUp(0))
        {
            isBeingWrongHeld = false;
            transform.DOMove(defaultPosition, 0.5f, false);
        }

        if(Input.GetMouseButtonUp(1))
        {
            if (Mathf.Abs(transform.position.x - targetPosition.x) <= 0.5f && Mathf.Abs(transform.position.y - targetPosition.y) <= 0.5)
            {
                defaultPosition = new Vector3(targetPosition.x, targetPosition.y);
                GameManager.Instance.Tongue.GetComponent<Tongue>().ChangeEffect();
                GameManager.Instance.FadeToBlack();
            }
            else
            {
                transform.DOMove(defaultPosition, 0.5f, false);
            }
            isBeingCorrectHeld = false;
        }
    }
}
