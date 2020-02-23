using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    public string player;

    private void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            if(!GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                transform.position += new Vector3(horizontal, 0, 0) * Time.deltaTime * movementSpeed;
            GetComponentInChildren<Animator>().SetInteger("Speed", (int)horizontal);

            if (Input.GetKeyDown(KeyCode.Space))
                GetComponentInChildren<Animator>().SetTrigger("Attack");
        }
    }
}
