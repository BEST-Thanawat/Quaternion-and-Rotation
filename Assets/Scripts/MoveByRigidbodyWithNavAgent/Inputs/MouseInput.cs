﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class MouseInput : MonoBehaviour
{
    //public float MovingTurnSpeed = 360;
    //public float StationaryTurnSpeed = 180;
    //public float ForwardAmount = 1f;
    //public CharacterControl characterControl;

    //private Mouse mouse;
    private InputMaster inputMaster;
    private InputAction.CallbackContext xx;
    private Vector2 MousePosition;
    private InputAction LeftMouseClick;
    private InputAction LeftMouseHold;
    private InputAction LeftMouseDoubleClick;
    private InputAction RightMouseClick;
    private InputAction RightMouseHold;
    private InputAction RightMouseDoubleClick;

    private bool TrigLMouseClick = false;
    private bool TrigRMouseClick = false;
    private bool TrigLMouseHold = false;
    private bool TrigRMouseHold = false;
    private bool TrigLMouseDoubleClick = false;
    private bool TrigRMouseDoubleClick = false;
    //private Quaternion deltaRotation;
    //private Rigidbody rb;
    private void Awake()
    {
        //mouse = new Mouse();
        //mouse = InputSystem.GetDevice<Mouse>();

        inputMaster = new InputMaster();
        inputMaster.Player.MousePosition.performed += context => MousePosition = context.ReadValue<Vector2>();

        //inputMaster.Player.MouseClick.performed += context => OnClicked(context);

        LeftMouseClick = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton", interactions: "press"); //(pressPoint=0.4)
        LeftMouseClick.performed += TrigLeftMouseClick;
        LeftMouseClick.canceled += UntrigLeftMouseClick;
        //LeftMouseClick.performed += context => VirtualInputManager.Instance.MouseLeftClicked = true;
        //LeftMouseClick.canceled += context => VirtualInputManager.Instance.MouseLeftClicked = false;

        RightMouseClick = new InputAction(type: InputActionType.Button, binding: "<Mouse>/rightButton", interactions: "press");
        RightMouseClick.performed += TrigRightMouseClick;
        RightMouseClick.canceled += UntrigRightMouseClick;

        LeftMouseHold = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton", interactions: "hold(duration=0.8)");
        LeftMouseHold.performed += context =>
        {
            TrigLMouseHold = true;
            VirtualInputManager.Instance.MouseLeftHold = true;
        };
        LeftMouseHold.canceled += context =>
        {
            TrigLMouseHold = false;
            VirtualInputManager.Instance.MouseLeftHold = false;
        };
        RightMouseHold = new InputAction(type: InputActionType.Button, binding: "<Mouse>/rightButton", interactions: "hold(duration=0.8)");
        RightMouseHold.performed += context =>
        {
            TrigRMouseHold = true;
            VirtualInputManager.Instance.MouseRightHold = true;
        };
        RightMouseHold.canceled += context =>
        {
            TrigRMouseHold = false;
            VirtualInputManager.Instance.MouseRightHold = false;
        };

        //LeftMouseDoubleClick = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton", interactions: "multitap(pressPoint=0.5, tapCount=2, tapDelay=0.5)");
        //LeftMouseDoubleClick.performed += TrigLeftMouseDoubleClick;
        //LeftMouseDoubleClick.canceled += UntrigLeftMouseDoubleClick;

        //RightMouseDoubleClick = new InputAction(type: InputActionType.Button, binding: "<Mouse>/rightButton", interactions: "multitap");
        //RightMouseDoubleClick.performed += context => TrigRMouseDoubleClick = true;
        //RightMouseDoubleClick.canceled += context => TrigRMouseDoubleClick = false;
    }

    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log("Click " + LeftMouseClick.triggered);
        //Debug.Log("Hold " + LeftMouseHold.triggered);
        //VirtualInputManager.Instance.MousePositionVector2 = MousePosition;//mouse.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(MousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Ground")))
        {
            VirtualInputManager.Instance.MousePositionVector3 = hit.point;

            if (TrigLMouseClick && !TrigLMouseHold)
            {
                VirtualInputManager.Instance.Attack = true;
                VirtualInputManager.Instance.ClickPosition = hit.point;
                VirtualInputManager.Instance.MouseLeftClicked = true;
            }
            else
            {
                VirtualInputManager.Instance.Attack = false;
                VirtualInputManager.Instance.MouseLeftClicked = false;
            }

            if (TrigRMouseClick && !TrigRMouseHold)
            {
                VirtualInputManager.Instance.ClickPosition = hit.point;
                VirtualInputManager.Instance.MouseRightClicked = true;
            }
            else
            {
                VirtualInputManager.Instance.MouseRightClicked = false;
            }
        }

        //if (LeftMouseHold.phase == InputActionPhase.Performed)
        //{
        //    VirtualInputManager.Instance.MouseLeftHold = true;
        //}

        //if (LeftMouseHold.phase == InputActionPhase.Canceled)
        //{
        //    VirtualInputManager.Instance.MouseLeftHold = false;
        //}

        //Debug.Log("H " + VirtualInputManager.Instance.MouseLeftHold);
        //else
        //{
        //    VirtualInputManager.Instance.Attack = false;
        //    VirtualInputManager.Instance.ClickPosition = Vector3.zero;
        //    VirtualInputManager.Instance.MouseClicked = false;
        //}
        ////VirtualInputManager.Instance.RotateValue = rotateValue;

        //if (Mouse.current.leftButton.isPressed)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        //Debug.Log(hit.point);
        //        VirtualInputManager.Instance.Attack = true;
        //        VirtualInputManager.Instance.ClickPosition = hit.point;
        //    }
        //}
        //else
        //{
        //    VirtualInputManager.Instance.Attack = false;
        //    VirtualInputManager.Instance.ClickPosition = Vector3.zero;
        //}
    }
    private void FixedUpdate()
    {
        //Ray ray = Camera.main.ScreenPointToRay(MousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        //{
        //    VirtualInputManager.Instance.MousePositionVector3 = hit.point;
        //}

        //Ray ray = Camera.main.ScreenPointToRay(characterControl.RotateValue);

        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, 100f))
        //{
        //    Vector3 move = characterControl.transform.InverseTransformPoint(hit.point);
        //    float m_TurnAmount = Mathf.Atan2(move.x, move.z);
        //    float turnSpeed = Mathf.Lerp(StationaryTurnSpeed, MovingTurnSpeed, ForwardAmount);
        //    deltaRotation = Quaternion.Euler(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
        //}
        //else
        //{
        //    deltaRotation = Quaternion.identity;
        //}

        ////Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 30, 0) * Time.deltaTime);
        //rb.MoveRotation(rb.rotation * deltaRotation);
    }
    private void TrigLeftMouseClick(InputAction.CallbackContext context)
    {
        TrigLMouseClick = true;
    }
    private void UntrigLeftMouseClick(InputAction.CallbackContext context)
    {
        TrigLMouseClick = false;
    }
    private void TrigRightMouseClick(InputAction.CallbackContext context)
    {
        TrigRMouseClick = true;
    }
    private void UntrigRightMouseClick(InputAction.CallbackContext context)
    {
        TrigRMouseClick = false;
    }
    private void TrigLeftMouseDoubleClick(InputAction.CallbackContext context)
    {
        TrigLMouseDoubleClick = true;
    }
    private void UntrigLeftMouseDoubleClick(InputAction.CallbackContext context)
    {
        TrigLMouseDoubleClick = false;
    }
    private void TrigRightMouseDoubleClick(InputAction.CallbackContext context)
    {
        TrigRMouseDoubleClick = true;
    }
    private void UntrigRightMouseDoubleClick(InputAction.CallbackContext context)
    {
        TrigRMouseDoubleClick = false;
    }
    private void OnEnable()
    {
        inputMaster.Player.Enable();
        LeftMouseClick.Enable();
        LeftMouseHold.Enable();
        RightMouseClick.Enable();
        RightMouseHold.Enable();
        //LeftMouseDoubleClick.Enable();
        //RightMouseDoubleClick.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Player.Disable();
        LeftMouseClick.Disable();
        LeftMouseHold.Disable();
        RightMouseClick.Disable();
        RightMouseHold.Disable();
        //LeftMouseDoubleClick.Disable();
        //RightMouseDoubleClick.Disable();
    }

    //private void OnDisable() => inputMaster.Player.Disable();
}
