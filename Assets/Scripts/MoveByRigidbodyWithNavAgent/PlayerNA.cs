using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using static InputMaster;

public class PlayerNA : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Text distanceToEnemy;

    //New Input System
    private InputMaster controls;
    private Keyboard keyboard;
    private Mouse mouse;

    Rigidbody rigidbody;

    //Movement
    private Vector2 movementInput;
    private Vector3 movement;
    private float speed = 4.5f;
    private Vector3 dirKeyBoardToMove;
    
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

    //Click to move
    [SerializeField] private GameObject instance;
    private InputAction.CallbackContext contextClickToMove;
    private RaycastHit hit;
    private bool trigClickToMove;
    private Vector3 dirClickToMove;

    private IEnumerator clickToMove;
    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.MouseClick.performed += context => contextClickToMove = context;
        controls.Player.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
        controls.Player.MousePosition.performed += context => turnInput = context.ReadValue<Vector2>();
        controls.Player.Jump.performed += context => contextJump = context;//OnAttack(context); //isJump = context.ReadValue<float>();
        //controls.Player.Jump.performed += context => Jump(context.ReadValue<float>(), jumpForce, forceMode);

        keyboard = InputSystem.GetDevice<Keyboard>();
        mouse = InputSystem.GetDevice<Mouse>();
        //Vector2 pos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
    }
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    

    private void Update()
    {
        GroundCheck();
        MovePlayerByMouse();
    }

    private void GroundCheck()
    {
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 10f, Color.blue);
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * GroundCheckDistance));
        RaycastHit raycastHit;
        //if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out raycastHit, GroundCheckDistance))
        {
            isJumping = false;
        }
        else
        {
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        MovePlayerByKeyBoard();

        if (!trigClickToMove)
        {
            RotatePlayer();
        }

        if (trigClickToMove)
        {
            ClickToMove(hit);
        }

        //if (trigClickToMove)
        //{
        //    relativePosition = hit.point - rigidbody.position;
        //    relativePosition.y = 0f;
        //    targetRotation = Quaternion.LookRotation(relativePosition);
        //    //Debug.Log(relativePosition);
        //    //Debug.Log(targetRotation);
        //    //Debug.Log(rotationTime);
        //    ClickToMove2(hit);
        //}

        if (!isJumping)
        {
            Jump(contextJump, jumpForce, forceMode);
        }
        isJumping = false;
    }

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
    private void MovePlayerByMouse()
    {
        if (Mouse.current.leftButton.isPressed && !isJumping)
        {
            Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue()); //contextClickToMove.ReadValue<Vector2>()
            if (Physics.Raycast(ray, out hit))
            {
                trigClickToMove = true;
            }
        }

        ////Prefer this way to move character
        //var targetInput = new Vector3(movementInput.x, 0, movementInput.y);
        //inputDirection = Vector3.Lerp(inputDirection, targetInput, Time.deltaTime * 10f);

        //movement.Set(inputDirection.x, 0f, inputDirection.z);
        //movement = movement * speed * Time.deltaTime;
        //rigidbody.MovePosition(transform.position + movement);
    }
    private void ClickToMove(RaycastHit raycastHit)
    {
        //Debug.Log(Vector3.Distance(transform.position, raycastHit.point));
        if (Vector3.Distance(transform.position, raycastHit.point) > 1f && trigClickToMove)
        {
            var targetInput = transform.InverseTransformPoint(raycastHit.point);
            //dirClickToMove = Vector3.Lerp(dirClickToMove, targetInput, Time.fixedDeltaTime * speed);
            rigidbody.MovePosition(Vector3.MoveTowards(transform.position, raycastHit.point, Time.fixedDeltaTime * speed)); //Time.deltaTime * dirClickToMove.magnitude

            //Rotate to target
            float turnSpeed = Mathf.Lerp(StationaryTurnSpeed, MovingTurnSpeed, ForwardAmount);
            float turnAmount = Mathf.Atan2(targetInput.x, targetInput.z);
            Quaternion deltaRotation = Quaternion.Euler(0, turnAmount * turnSpeed * Time.fixedDeltaTime, 0);
            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        }
        else
        {
            trigClickToMove = false;
        }
    }

    Vector3 relativePosition;
    Quaternion targetRotation = Quaternion.identity;
    float rotationTime;
    private void ClickToMove2(RaycastHit raycastHit)
    {
        if (Vector3.Distance(transform.position, raycastHit.point) > 1f && trigClickToMove && rotationTime < 1)
        {
            var targetInput = transform.InverseTransformPoint(raycastHit.point);
            dirClickToMove = Vector3.Lerp(dirClickToMove, targetInput, Time.fixedDeltaTime * speed);
            rigidbody.MovePosition(Vector3.MoveTowards(transform.position, raycastHit.point, Time.fixedDeltaTime * speed));

            rotationTime += Time.deltaTime * 0.1f;
            rigidbody.MoveRotation(Quaternion.Lerp(rigidbody.rotation, targetRotation, rotationTime));
        }
        else
        {
            trigClickToMove = false;
        }

        if(rotationTime > 1) rotationTime = 0;
    }

    //***Click to move using coroutine
    //IEnumerator ClickToMove(RaycastHit raycastHit)
    //{
    //    while (Vector3.Distance(transform.position, raycastHit.point) > 1f) //(Vector3.Distance(raycastHit.point, transform.position) > 1f)
    //    {
    //        //Debug.Log("Click " + raycastHit.point.x + " " + "Pos " + transform.position.x);
    //        //Instantiate(instance, hit.point, Quaternion.identity);
    //        var targetInput = transform.InverseTransformPoint(raycastHit.point);

    //        rigidbody.MovePosition(Vector3.MoveTowards(transform.position, raycastHit.point, targetInput.magnitude * Time.deltaTime));
    //        yield return null;
    //    }

    //    if (Vector3.Distance(transform.position, raycastHit.point) <= 1f)
    //        isMove = false;
    //}

    private void MovePlayerByKeyBoard()
    {
        //if (isJumping) return;
        if (movementInput != Vector2.zero) trigClickToMove = false;

        //Prefer this way to move character
        var targetInput = new Vector3(movementInput.x, 0, movementInput.y);
        dirKeyBoardToMove = Vector3.Lerp(dirKeyBoardToMove, targetInput, Time.deltaTime * speed);

        movement.Set(dirKeyBoardToMove.x, 0f, dirKeyBoardToMove.z);
        movement = movement * speed * Time.deltaTime;
        if (movement != Vector3.zero) rigidbody.MovePosition(transform.position + movement);
        else rigidbody.angularVelocity = Vector3.zero; //Stop rotate in case of mouse pointer is null

        //Other way to move character
        //rigidbody.MovePosition(transform.position + Time.deltaTime * speed * transform.TransformDirection(movementInput.x, 0, movementInput.y));
    }

    private void RotatePlayer()
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
