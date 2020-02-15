using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    SpriteRenderer SpriteRenderer;

    public Sprite handCursor;
    public Sprite normalCursor;

    Vector2 cursorPosition;

    Vector3 offset = new Vector3(1.5f, -7, 0);

    void Start()
    {
        Cursor.visible = false;
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + offset);
        transform.position = cursorPosition;

        if(Input.GetMouseButtonDown(0))
        {
            SpriteRenderer.sprite = handCursor;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            SpriteRenderer.sprite = normalCursor;
        }
    }
}
