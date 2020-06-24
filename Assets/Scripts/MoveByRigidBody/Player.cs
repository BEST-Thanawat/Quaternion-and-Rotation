using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Text distanceToEnemy;

    //New Input System
    private InputMaster controls;
    private Keyboard kb;

    Rigidbody rigidbody;

    //Movement
    private Vector2 movementInput;
    private Vector3 movement;
    private float speed = 4.5f;
    private Vector3 inputDirection;

   
    //Turn
    private Vector2 turnInput;
    [Header("Rotation")]
    [SerializeField] private float MovingTurnSpeed = 360;
    [SerializeField] private float StationaryTurnSpeed = 180;
    [SerializeField] private float ForwardAmount = 1f;

    //Jump
    [Header("Jump")]
    [SerializeField] private float jumpForce = 3000f;
    [SerializeField] private ForceMode forceMode = ForceMode.Force;
    private InputAction.CallbackContext contextJump;
    private bool isJumping = false;
    private Vector3 groundLocation;
    public float GroundCheckDistance = 1.3f;

    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Shoot.performed += context => Shoot();
        controls.Player.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
        controls.Player.Turn.performed += context => turnInput = context.ReadValue<Vector2>();
        controls.Player.Jump.performed += context => contextJump = context;//OnAttack(context); //isJump = context.ReadValue<float>();
        //controls.Player.Jump.performed += context => Jump(context.ReadValue<float>(), jumpForce, forceMode);

        kb = InputSystem.GetDevice<Keyboard>();
        //Vector2 pos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
    }
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    

    private void Update()
    {
        GroundCheck();
    }

    private void GroundCheck()
    {
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 10f, Color.blue);
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * GroundCheckDistance));
        RaycastHit hit;
        //if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hit, GroundCheckDistance))
        {

            //if (string.Compare(hit.collider.tag, "Ground", StringComparison.Ordinal) == 0)
            //{
            //    groundLocation = hit.point;
            //}

            //var distanceFromPlayerToGround = Vector3.Distance(transform.position, groundLocation);
            //if (distanceFromPlayerToGround >= GroundCheckDistance) //1.1f
            //{
            //    isJumping = true;
            //}
            //Debug.Log(distanceFromPlayerToGround);
            isJumping = false;
        }
        else
        {
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        MoveThePlayer();
        TurnThePlayer();

        if (!isJumping)
        {
            Jump(contextJump, jumpForce, forceMode);
        }
        isJumping = false;
        //Jump(isJump, jumpForce, ForceMode.Impulse);
        //if (kb.spaceKey.isPressed)
        //{
        //    //Debug.Log(kb.spaceKey.isPressed);
        //    //Cursor.lockState = CursorLockMode.None;
        //    Jump(jumpForce, forceMode);
        //}
    }

    //void OnJump(InputAction.CallbackContext context)
    //{
    //    switch (context.phase)
    //    {
    //        case InputActionPhase.Started:
    //            /* the space key was pressed */
    //            Debug.Log("the space key was pressed");
    //            isJump = true;
    //            break;

    //        case InputActionPhase.Performed:
    //            /* the space key was held for 3 seconds */
    //            Debug.Log("the space key was performed");
    //            break;

    //        case InputActionPhase.Canceled:
    //            /* the space key was released; if there was no 'Performed', it was released before 3 seconds were up */
    //            Debug.Log("the space key was released");
    //            break;
    //    }
    //}

    private void Jump(InputAction.CallbackContext context, float jumpForce, ForceMode forceMode)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                /* the space key was pressed */
                //Debug.Log("the space key was pressed");
                rigidbody.AddForce(jumpForce * rigidbody.mass * Time.deltaTime * Vector3.up, forceMode);
                break;
            //case InputActionPhase.Performed:
            //    /* the space key was held for 3 seconds */
            //    //Debug.Log("the space key was performed");
            //    break;

            //case InputActionPhase.Canceled:
            //    /* the space key was released; if there was no 'Performed', it was released before 3 seconds were up */
            //    //Debug.Log("the space key was released");
            //    break;
        }
    }
    private void MoveThePlayer()
    {
        //Prefer this way to move character
        var targetInput = new Vector3(movementInput.x, 0, movementInput.y);
        inputDirection = Vector3.Lerp(inputDirection, targetInput, Time.deltaTime * 10f);

        movement.Set(inputDirection.x, 0f, inputDirection.z);
        movement = movement * speed * Time.deltaTime;
        rigidbody.MovePosition(transform.position + movement);

        //Other way to move character
        //rigidbody.MovePosition(transform.position + Time.deltaTime * speed * transform.TransformDirection(movementInput.x, 0, movementInput.y));
    }

    private void TurnThePlayer()
    { 
        Quaternion deltaRotation = Quaternion.identity;
        Ray ray = Camera.main.ScreenPointToRay(turnInput);
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            Vector3 move = transform.InverseTransformPoint(hit.point);
            float m_TurnAmount = Mathf.Atan2(move.x, move.z);
            float turnSpeed = Mathf.Lerp(StationaryTurnSpeed, MovingTurnSpeed, ForwardAmount);
            deltaRotation = Quaternion.Euler(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
        }

        //Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 30, 0) * Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();

    public void Shoot()
    {
        Debug.Log("Shoot");
    }
}
