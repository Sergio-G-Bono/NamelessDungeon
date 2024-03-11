using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_PlayerMovement : MonoBehaviour
{

    IA_Input inputClass;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovPressed;
    CharacterController cc;
    Animator animator;
    [SerializeField] float rotationFactorPerFrame = 1.0f;

    [SerializeField] private bool useAddForce = false;
    [SerializeField] private float speed;
    [SerializeField] private Transform orientation;

    private float horizInput;
    private float vertInput;

    private Vector3 moveDir;

    private Rigidbody rb;

    private void Awake()
    {
        inputClass =new IA_Input();
        cc = GetComponent<CharacterController>();
        animator=GetComponent<Animator>();

        inputClass.AM_CharControl.Move.started += ctx => 
        {
            OnMovementInput(ctx);
        };
        inputClass.AM_CharControl.Move.canceled += ctx =>
        {
            OnMovementInput(ctx);
        };
        inputClass.AM_CharControl.Move.performed += ctx =>
        {
            OnMovementInput(ctx);
        };
    }
    private void OnEnable()
    {
        inputClass.Enable();
    }
    private void OnDisable()
    {
        inputClass.Disable();
    }
    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //rb.freezeRotation = true;
        //Debug.Log(rb);
    }

    private void Update()
    {
        // GetInput();
        RotatePlayer();
        Move();
        HandleAnimations();
    }
    private void FixedUpdate()
    {
        //MovePlayer();
        //RotatePlayer();
        //SpeedControl();
    }

    private void OnMovementInput(InputAction.CallbackContext ctx)
    {
        currentMovementInput = ctx.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    private void RotatePlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            Vector3 dotherotate = new Vector3(raycastHit.point.x, transform.transform.position.y, raycastHit.point.z);
            Quaternion currentRot = transform.rotation;

            //Debug.Log(transform.forward);
            transform.LookAt(dotherotate);
            //transform.Rotate(new Vector3(0,dotherotate.y,0));
        }

        // transform.rotation=Quaternion.Euler(0,0,0); 
        
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Physics.Raycast(ray, out RaycastHit raycastHit))
        //{
        //    Vector3 direction = new Vector3(raycastHit.point.x, transform.transform.position.y, raycastHit.point.z);
        //    float angleRad = Mathf.Atan2(direction.y, direction.x);
        //    float angleDeg = angleRad * Mathf.Rad2Deg;
        //    Quaternion targetRotation = Quaternion.Euler(0f, angleDeg, 0f);

        //    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        //}

    }

    //private void GetInput()
    //{
    //    horizInput = Input.GetAxisRaw("Horizontal");
    //    vertInput = Input.GetAxisRaw("Vertical");
    //}

    //private void MovePlayer()
    //{
    //    moveDir=Vector3.forward*vertInput+Vector3.right*horizInput;
    //    if (useAddForce)
    //    {
    //        rb.AddForce(moveDir.normalized * speed);
    //    }
    //    else
    //    {
    //        rb.velocity = moveDir * speed;
    //    }
    //}
    //private void SpeedControl()
    //{
    //    Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

    //    if (flatVel.magnitude>speed)
    //    {
    //        Vector3 limVel=flatVel.normalized*speed;
    //        rb.velocity = new Vector3(limVel.x,rb.velocity.y,limVel.z);
    //    }
    //}
    private void Move()
    {
        cc.SimpleMove(currentMovement*1*speed);
    }
    private void HandleAnimations()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");

        if(isMovPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }

        if (!isMovPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }
    }
    private void HandleRotation()
    {
        Vector3 posToLookAt;
        posToLookAt.x = currentMovement.x;
        posToLookAt.y = 0;
        posToLookAt.z = currentMovement.z;
        Quaternion currentRot = transform.rotation;
        if (isMovPressed)
        {
            Quaternion targetRot = Quaternion.LookRotation(posToLookAt);
            Quaternion.Slerp(currentRot, targetRot, rotationFactorPerFrame);

        }
    }
}
