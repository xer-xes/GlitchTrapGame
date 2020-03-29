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
        {       //---------------------------------- FOR PLAYER2
            if (transform.parent.GetComponent<PlayerMovement>().player == "Player2")
            {
                if (isAttacking && hit && collision.gameObject != this.transform.parent.gameObject &&
                    (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Left" || collision.gameObject.tag == "LeftTower")) 
                {
                    hit = false;
                    if (collision.gameObject.GetComponent<PlayerMovement>() != null && collision.gameObject != this.transform.parent.gameObject)
                    {
                        collision.gameObject.GetComponent<PlayerMovement>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, transform.parent.GetComponent<PlayerMovement>().damage);
                    }
                    if (collision.gameObject.tag == "Left")
                    {
                        collision.gameObject.GetComponent<CreepsBehaviourScript>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All,
                                                                                                                  transform.parent.GetComponent<PlayerMovement>().damage);
                    }
                    if (collision.gameObject.tag == "LeftTower")
                    {
                            collision.gameObject.GetComponentInChildren<TowerRangeFinder>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All,
                                                                                                                  transform.parent.GetComponent<PlayerMovement>().damage);
                    }
                }
            }
            if (transform.parent.GetComponent<PlayerMovement>().player == "Player1")
            {       //--------------------------------- FOR PLAYER1
                if (isAttacking && hit && collision.gameObject != this.transform.parent.gameObject &&
                    (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Right" || collision.gameObject.tag == "RightTower")) 
                {
                    hit = false;
                    if (collision.gameObject.GetComponent<PlayerMovement>() != null && collision.gameObject != this.transform.parent.gameObject)
                    {
                        collision.gameObject.GetComponent<PlayerMovement>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, transform.parent.GetComponent<PlayerMovement>().damage);
                    }
                    if (collision.gameObject.tag == "Right")
                    {
                        collision.gameObject.GetComponent<CreepsBehaviourScript>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All,
                                                                                                                  transform.parent.GetComponent<PlayerMovement>().damage);
                    }
                    if (collision.gameObject.tag == "RightTower")
                    {
                            collision.gameObject.GetComponentInChildren<TowerRangeFinder>().GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All,
                                                                                                                  transform.parent.GetComponent<PlayerMovement>().damage);
                    }
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
    {       //------------------------ DEATH TIME
        GetComponent<SpriteRenderer>().enabled = false;
        if (transform.parent.GetComponent<PlayerMovement>().player == "Player1")
            transform.parent.position = Vector3.zero + playerSpawnPosition.localPosition;
        else
            transform.parent.position = Vector3.zero - playerSpawnPosition.localPosition;
        StartCoroutine(Respawn(10));
    }

    [PunRPC]
    private IEnumerator Respawn(int time)
    {       //------------------------------ RESPAWN TIME
        yield return new WaitForSecondsRealtime(time);
        GetComponent<Animator>().ResetTrigger("Dead");
        GetComponent<Animator>().SetTrigger("Respawn");
        GetComponent<SpriteRenderer>().enabled = true;
        transform.parent.GetComponent<PlayerMovement>().dead = false;
        transform.parent.GetComponent<PlayerMovement>().health = transform.parent.GetComponent<PlayerMovement>().maxHealth;
        foreach (var health in GetComponentsInChildren<SimpleHealthBar>())
            if (health.type == "Health")
                health.gameObject.GetComponent<Image>().fillAmount = (float)transform.parent.GetComponent<PlayerMovement>().health /
                    transform.parent.GetComponent < PlayerMovement>().maxHealth;
    }
}
