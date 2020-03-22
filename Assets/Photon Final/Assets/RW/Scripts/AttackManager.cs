using UnityEngine;
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
                (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Left"))
            {
                hit = false;
                if (collision.gameObject.GetComponent<PlayerMovement>() != null && collision.gameObject != this.transform.parent.gameObject)
                {
                    collision.gameObject.GetComponent<PlayerMovement>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, transform.parent.GetComponent<PlayerMovement>().damage);
                }
                if(collision.gameObject.tag == "Left")
                {
                    collision.gameObject.GetComponent<CreepsBehaviourScript>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 
                                                                                                              transform.parent.GetComponent<PlayerMovement>().damage);
                }
            }
        }
    }

    [PunRPC]
    public void Attack()
    {
        GetComponent<Animator>().SetTrigger("Attack");
    }

    [PunRPC]
    public void Dead()
    {
        GetComponent<Animator>().SetTrigger("Dead");
    }

    [PunRPC]
    public void Walking(int speed)
    {
        GetComponent<Animator>().SetInteger("Speed", speed);
    }
}
