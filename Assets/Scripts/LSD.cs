using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LSD : MonoBehaviour
{
    private bool isBeingWrongHeld = false;
    public bool isBeingCorrectHeld = false;

    Vector3 defaultPosition;

    Vector3 mousePosition;
    Vector3 offset = new Vector3(1.5f, -7, 0);
    void Start()
    {        
        defaultPosition = gameObject.transform.localPosition;
    }
        
    void Update()
    {
        if(isBeingWrongHeld == true || isBeingCorrectHeld == true)
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition + offset);
            gameObject.transform.localPosition = new Vector3(mousePosition.x, mousePosition.y, 0);            
        }

        if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            if(isBeingWrongHeld == true)
            {
                isBeingWrongHeld = false;
            }

            if(isBeingCorrectHeld == true)
            {
                isBeingCorrectHeld = false;
            }

            if (transform.localPosition != defaultPosition)
            {
                transform.DOMove(defaultPosition, 0.5f, false);
            }
        }
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isBeingWrongHeld = true;
        }

        if(Input.GetMouseButtonDown(1))
        {
            isBeingCorrectHeld = true;
        }
    }
}
