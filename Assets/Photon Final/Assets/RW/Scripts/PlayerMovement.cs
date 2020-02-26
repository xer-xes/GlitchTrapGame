using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
   
    public string player;
    private bool isFacingForward = true;
 
    private void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            this.GetComponentInChildren<SpriteRenderer>().sortingOrder = 1;
            float horizontal = Input.GetAxisRaw("Horizontal");
            if (player == "Player1")
            {
                if (horizontal < 0 && isFacingForward)
                {
                    GetComponentInChildren<SpriteRenderer>().flipX = true;
                    isFacingForward = false;
                }
                else if (horizontal > 0 && !isFacingForward)
                {
                    GetComponentInChildren<SpriteRenderer>().flipX = false;
                    isFacingForward = true;
                }
            }
            else if(player == "Player2")
            {
                if (horizontal > 0 && isFacingForward)
                {
                    GetComponentInChildren<SpriteRenderer>().flipX = true;
                    isFacingForward = false;
                }
                else if (horizontal < 0 && !isFacingForward)
                {
                    GetComponentInChildren<SpriteRenderer>().flipX = false;
                    isFacingForward = true;
                }
            }

            if(!GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                transform.position += new Vector3(horizontal, 0, 0) * Time.deltaTime * movementSpeed;
            GetComponentInChildren<Animator>().SetInteger("Speed", (int)horizontal);

            if (Input.GetKeyDown(KeyCode.Space))
                GetComponentInChildren<Animator>().SetTrigger("Attack");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("In Coliision : " + gameObject.name);
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Debug.Log("Ignore");
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<BoxCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
        }
    }
}
