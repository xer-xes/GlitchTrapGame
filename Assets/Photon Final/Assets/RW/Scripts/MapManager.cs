using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject skeletons;

    private void Start()
    {
        foreach (SpriteRenderer skeleton in skeletons.GetComponentsInChildren<SpriteRenderer>())
        {
            byte rand = (byte)(Random.Range(50, 200) / 255);
            skeleton.color = new Color32(rand, rand, rand, 1);
        }
    }
}
