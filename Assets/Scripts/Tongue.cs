using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeEffect()
    {
        //Destroy(GetComponent<_2dxFX_AL_4Gradients>());
        gameObject.AddComponent<_2dxFX_AL_Hologram>();
    }
}
