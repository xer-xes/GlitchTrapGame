using UnityEngine;
using Photon.Pun;
using System.Collections;
using UnityEngine.UI;

public class AttackManager : MonoBehaviour
{
    private bool isAttacking = false;
    private bool hit = false;
    [SerializeField] private Transform playerSpawnPosition;

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

    [PunRPC]
    public void Respawn()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        transform.parent.position = playerSpawnPosition.position;
        transform.parent.GetComponent<Rigidbody2D>().gravityScale = 0;
        StartCoroutine(Respawn(10));
    }

    [PunRPC]
    private IEnumerator Respawn(int time)
    {
        yield return new WaitForSecondsRealtime(time);
        GetComponent<Animator>().ResetTrigger("Dead");
        GetComponent<Animator>().SetTrigger("Respawn");
        GetComponent<SpriteRenderer>().enabled = true;
        transform.parent.GetComponent<Rigidbody2D>().gravityScale = 1;
        transform.parent.GetComponent<PlayerMovement>().dead = false;
        transform.parent.GetComponent<PlayerMovement>().health = transform.parent.GetComponent<PlayerMovement>().maxHealth;
        foreach (var health in GetComponentsInChildren<SimpleHealthBar>())
            if (health.type == "Health")
                health.gameObject.GetComponent<Image>().fillAmount = (float)transform.parent.GetComponent<PlayerMovement>().health /
                    transform.parent.GetComponent < PlayerMovement>().maxHealth;
    }
}
