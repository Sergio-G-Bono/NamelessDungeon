using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sc_PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool useAddForce = false;
    [SerializeField] private float speed;
    [SerializeField] private Transform orientation;

    private float horizInput;
    private float vertInput;

    private Vector3 moveDir;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Debug.Log(rb);
    }

    private void Update()
    {
        GetInput();
    }
    private void FixedUpdate()
    {
        MovePlayer();
        SpeedControl();
    }

    private void GetInput()
    {
        horizInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDir=orientation.forward*vertInput+orientation.right*horizInput;
        if (useAddForce)
        {
            rb.AddForce(moveDir.normalized * speed);
        }
        else
        {
            rb.velocity = moveDir * speed;
        }
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude>speed)
        {
            Vector3 limVel=flatVel.normalized*speed;
            rb.velocity = new Vector3(limVel.x,rb.velocity.y,limVel.z);
        }
    }
}
