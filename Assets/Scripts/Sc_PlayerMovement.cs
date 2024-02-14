using System;
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
        RotatePlayer();
        SpeedControl();
    }

    private void RotatePlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            Vector3 dotherotate = new Vector3(raycastHit.point.x, transform.transform.position.y, raycastHit.point.z);
            //transform.rotation = Quaternion.Euler(new Vector3(0, dotherotate.y, 0));
            transform.LookAt(dotherotate);
            //transform.Rotate(new Vector3(0,dotherotate.y,0));
        }
       // transform.rotation=Quaternion.Euler(0,0,0); 
    }

    private void GetInput()
    {
        horizInput = Input.GetAxisRaw("Horizontal");
        vertInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDir=Vector3.forward*vertInput+Vector3.right*horizInput;
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
