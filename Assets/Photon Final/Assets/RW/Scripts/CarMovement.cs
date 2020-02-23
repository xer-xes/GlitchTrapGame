using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon;
using Photon.Pun;

public class CarMovement : MonoBehaviour
{
    [Range(5f, 50f)]
    public int speed = 15;

    [Range(90f, 150f)]
    public int turnSpeed = 120;

    [Range(5f, 50f)]
    public float velocity = 20f;

    private string movementAxisName;
    private string turnAxisName;
    private string playerName = "";

    private Rigidbody rb;

    private float movementInputValue;
    private float turnInputValue;
    private float originalPitch;

    PhotonView photonView;

    private void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        playerName = photonView.Owner.NickName;
        gameObject.name = playerName;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        movementInputValue = 0f;
        turnInputValue = 0f;
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            movementInputValue = Input.GetAxis("Vertical");
            turnInputValue = Input.GetAxis("Horizontal");
        }
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void Turn()
    {
        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}
