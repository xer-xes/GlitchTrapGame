using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class CreepsBehaviourScript : MonoBehaviour
{
    public string type;
    public bool isAttacking = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.gameObject.tag == "Left")
        {
            if (collision.gameObject.tag == "Right" ||
                 (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2"))
            {
                isAttacking = true;
                if (collision.gameObject.tag == "Right")
                    collision.gameObject.GetComponent<CreepsBehaviourScript>().isAttacking = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.gameObject.tag == "Left")
        {
            if (collision.gameObject.tag == "Right" ||
                (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerMovement>().player == "Player2"))
            {
                isAttacking = false;
                if (collision.gameObject.tag == "Right")
                    collision.gameObject.GetComponent<CreepsBehaviourScript>().isAttacking = false;
            }
        }
    }

    private void Update()
    {
        GetComponent<Animator>().SetBool("Attack", isAttacking);
    }

    public void LaunchBullet()
    {

    }
}
