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
	[SerializeField] private Transform orientation;

	private float horizInput;
	private float vertInput;

	private Vector3 moveDir;

	private Rigidbody rb;

	private void Awake()
	{
		inputClass = new IA_Input();
		cc = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();

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

	private void OnMovementInput(InputAction.CallbackContext ctx)
	{
		currentMovementInput = ctx.ReadValue<Vector2>();
		currentMovement.x = currentMovementInput.x ;
		currentMovement.z = currentMovementInput.y;

		isMovPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
	}

	private void RotatePlayer()
	{
		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		//if (Physics.Raycast(ray, out RaycastHit raycastHit))
		//{
		//	Vector3 dotherotate = new Vector3(raycastHit.point.x, transform.transform.position.y, raycastHit.point.z);
		//	Quaternion currentRot = transform.rotation;

		//	//Debug.Log(transform.forward);
		//	transform.LookAt(dotherotate);
		//	//transform.Rotate(new Vector3(0,dotherotate.y,0));
		//}

        // normalise input direction
        Vector3 inputDirection = new Vector3(currentMovementInput.x, 0.0f, currentMovementInput.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (currentMovementInput != Vector2.zero)
        {
           float _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +cam.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref rotationFactorPerFrame,.1f);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }


    }


    private void Move()
	{
		//Debug.Log(currentMovement);
		var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, -45, 0));
		var skewedInput = matrix.MultiplyPoint3x4(currentMovement);
		cc.SimpleMove(skewedInput * 1 * speed);
	}
	private void HandleAnimations()
	{
		bool isWalking = animator.GetBool("isWalking");
		bool isRunning = animator.GetBool("isRunning");

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
