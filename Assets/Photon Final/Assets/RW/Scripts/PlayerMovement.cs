using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;

    private void Update()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            Debug.Log("horizontal : " + (int)horizontal);
            transform.position += new Vector3(horizontal, 0, 0) * Time.deltaTime * movementSpeed;
            GetComponentInChildren<Animator>().SetInteger("Speed", (int)horizontal); 
        }
    }
}
