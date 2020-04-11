using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject skeletons;

    private void Start()
    {
        int rand;
        foreach (SpriteRenderer skeleton in skeletons.GetComponentsInChildren<SpriteRenderer>())
        {
            rand = Random.Range(100, 200);
            skeleton.color = new Color32((byte)rand, (byte)rand, (byte)rand, 255);
        }
    }
}
