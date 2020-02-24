using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private GameObject otherPlayer;
    public string player;
    private bool isFacingForward = true;

    private void Start()
    {
        if(GetComponent<PhotonView>().IsMine)
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), otherPlayer.GetComponent<BoxCollider2D>());
        }
    }

    private void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
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
}
