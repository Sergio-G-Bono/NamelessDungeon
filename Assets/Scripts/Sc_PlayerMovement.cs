using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Sc_PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    IA_Input inputClass;
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovPressed;
    CharacterController cc;
    Animator animator;
    float rotationFactorPerFrame = 1.0f;

    [SerializeField] private bool useAddForce = false;
    [SerializeField] private float speed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minMovementDistanceFromPlayer = 1f;
    [SerializeField] private float slowMovementDistanceFromPlayer = 8f;
    //   [SerializeField] private float fastMovementDistanceFromPlayer = 1f;
    [SerializeField] private float smoothRotValue = .1f;
    [SerializeField] private Transform orientation;

    private float horizInput;
    private float vertInput;

    private Vector3 moveDir;

    private Rigidbody rb;
    private bool isMousePointing = false;
    private Vector2 mousePointPos;
    private bool isPadConnected = false;
    private float distanceFromTarget;

    private void Awake()
    {
        inputClass = new IA_Input();
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        //controller
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

        //mouse control
        inputClass.AM_CharControl.PointMouseMov.canceled += ctx =>
        {
            OnMovementTargetInpuReleased(ctx);
        };

        inputClass.AM_CharControl.PointMouseMov.performed += ctx =>
        {
            OnMovementTargetInput(ctx);
        };

        InputSystem.onDeviceChange += (device, change) =>
        {
            DeviceChange(device, change);
        };

        //checks gamepad
        if (Gamepad.current != null)
        {
            isPadConnected = true;
        }

    }

    private void DeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Added:
                // New Device.
                Debug.Log("Added");
                isPadConnected = true;
                break;
            case InputDeviceChange.Disconnected:
                // Device got unplugged.
                Debug.Log("disconnected");
                isPadConnected = false;
                break;
            //case InputDeviceChange.Reconnected://was connected obsolete?
            //    // Plugged back in.
            //    Debug.Log("Reconnected");
            //    break;
            //case InputDeviceChange.Removed:
            //    // Remove from Input System entirely; by default, Devices stay in the system once discovered.
            //    Debug.Log("Removed");
            //    break;
            default:
                // See InputDeviceChange reference for other event types.
                break;
        }
    }


    private void OnMovementTargetInpuReleased(InputAction.CallbackContext ctx)
    {
        isMousePointing = false;
        animator.SetFloat("Speed", 0f);
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
    }

    private void OnMovementTargetInput(InputAction.CallbackContext ctx)
    {
        isMousePointing = true;
    }

    private void OnEnable()
    {
        inputClass.Enable();
    }
    private void OnDisable()
    {
        inputClass.Disable();
    }


    private void Update()
    {
        RotatePlayer();
        Move();
        HandleAnimations();
    }

    private void OnMovementInput(InputAction.CallbackContext ctx)
    {
        if (isPadConnected)
        {
            currentMovementInput = ctx.ReadValue<Vector2>();
            currentMovement.x = currentMovementInput.x;
            currentMovement.z = currentMovementInput.y;

            isMovPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
        }

    }

    private void RotatePlayer()
    {
        if (isMousePointing && !isPadConnected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                Vector3 dotherotate = new Vector3(raycastHit.point.x, transform.transform.position.y, raycastHit.point.z);
                //transform.LookAt(dotherotate);

                var targetRotation = Quaternion.LookRotation(dotherotate - transform.position);

                // Smoothly rotate towards the target point.
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothRotValue);

                distanceFromTarget = Vector2.Distance(transform.position, dotherotate);
            }
        }
        else
        {
            // normalise input direction
            Vector3 inputDirection = new Vector3(currentMovementInput.x, 0.0f, currentMovementInput.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (currentMovementInput != Vector2.zero)
            {
                float _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref rotationFactorPerFrame, smoothRotValue);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
        }
    }


    private void Move()
    {
        if (isMousePointing && !isPadConnected)
        {
            if (distanceFromTarget > minMovementDistanceFromPlayer)
            {
                Debug.Log(distanceFromTarget);
                if (distanceFromTarget > slowMovementDistanceFromPlayer)
                {
                    speed = maxSpeed;
                    Debug.Log("fdfdsfsfsf");
                    animator.SetFloat("Speed", .6f);
                    animator.SetBool("isWalking", false);
                    animator.SetBool("isRunning", true);
                }
                else if (distanceFromTarget > minMovementDistanceFromPlayer)
                {
                    speed = minSpeed;
                    animator.SetFloat("Speed", .4f);
                    animator.SetBool("isRunning", false);
                    animator.SetBool("isWalking", true);
                }


                cc.SimpleMove(transform.forward * 1 * speed);
            }
        }
        else
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, -45, 0));
            var skewedInput = matrix.MultiplyPoint3x4(currentMovement);
            cc.SimpleMove(skewedInput * 1 * speed);
        }

    }

    private void HandleAnimations()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");

        if (isMousePointing && !isPadConnected && distanceFromTarget > minMovementDistanceFromPlayer)
        {

           // animator.SetBool("isWalking", true);
        }
        else
        {
            if (isMovPressed && !isWalking)
            {
                animator.SetBool("isWalking", true);
            }

            if (!isMovPressed && isWalking)
            {
                animator.SetBool("isWalking", false);
                currentMovement = Vector3.zero;
            }
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
