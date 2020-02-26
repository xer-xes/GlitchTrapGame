using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class AttackManager : MonoBehaviour
{
    private bool isAttacking = false;
    private bool hit = false;

    public void CheckHit()
    {
        isAttacking = true;
        hit = true;
    }

    public void UnCheckHit()
    {
        isAttacking = false;
        hit = false;
    }

    private void OnTriggerStay2D (Collider2D collision)
    {
        if (transform.parent.gameObject.GetComponent<PhotonView>().IsMine)
        {
            if (isAttacking && hit && collision.gameObject != this.transform.parent.gameObject &&
                (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Opponent"))
            {
                hit = false;
                if (collision.gameObject.GetComponent<PlayerMovement>() != null && collision.gameObject != this.transform.parent.gameObject)
                {
                    collision.gameObject.GetComponent<PlayerMovement>().TakeDamage();
                }
            }
        }
    }
}
