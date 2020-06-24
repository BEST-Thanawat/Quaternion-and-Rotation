using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Text distanceToEnemy;

    //New Input System
    private InputMaster controls;

    Rigidbody rigidbody;

    //Movement
    private Vector2 movementInput;
    private Vector3 movement;
    private float speed = 4.5f;
    private Vector3 inputDirection;

    //Turn
    private Vector2 turnInput;
    [SerializeField] private float MovingTurnSpeed = 360;
    [SerializeField] private float StationaryTurnSpeed = 180;
    [SerializeField] private float ForwardAmount = 1f;

    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Shoot.performed += context => Shoot();
        controls.Player.Movement.performed += context => movementInput = context.ReadValue<Vector2>();
        controls.Player.Turn.performed += context => turnInput = context.ReadValue<Vector2>();
        //Vector2 pos = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
    }
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    

    private void Update()
    {

    }
    void FixedUpdate()
    {
        MoveThePlayer();
        TurnThePlayer();
    }

    private void MoveThePlayer()
    {
        var targetInput = new Vector3(movementInput.x, 0, movementInput.y);
        inputDirection = Vector3.Lerp(inputDirection, targetInput, Time.deltaTime * 10f);

        movement.Set(inputDirection.x, 0f, inputDirection.z);
        movement = movement * speed * Time.deltaTime;
        rigidbody.MovePosition(transform.position + movement);

        //rigidbody.velocity = new Vector3(desiredDirection.x, rigidbody.velocity.y, desiredDirection.y);
    }

    void TurnThePlayer()
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
