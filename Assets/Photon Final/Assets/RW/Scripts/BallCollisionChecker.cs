using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallCollisionChecker : MonoBehaviour
{
    public string lastTouchPlayer = "";
    public GameObject winnerUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            lastTouchPlayer = other.name;
            Debug.Log("Last Touch : " + lastTouchPlayer);
        }
    }
}
