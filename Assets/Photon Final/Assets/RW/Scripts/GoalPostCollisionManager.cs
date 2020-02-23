using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalPostCollisionManager : MonoBehaviour
{
    public GameObject ballCollision;
    public GameObject winnerUI;
    BallCollisionChecker ballCollisionChecker;

    // Use this for initialization
    void Start()
    {
        winnerUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            try
            {
                ballCollision = GameObject.Find("Ball");
                ballCollisionChecker = ballCollision.transform.GetChild(0).gameObject.GetComponent<BallCollisionChecker>();
            }
            catch
            {
                ballCollision = GameObject.Find("Ball(Clone)");
                ballCollisionChecker = ballCollision.transform.GetChild(0).gameObject.GetComponent<BallCollisionChecker>();
            }

            string lastTouchPlayer = ballCollisionChecker.lastTouchPlayer;
            Debug.Log("Goal by " + lastTouchPlayer);

            winnerUI.SetActive(true);
            winnerUI.transform.GetChild(0).GetComponent<Text>().text = lastTouchPlayer + " Scored!";
        }
    }
}
