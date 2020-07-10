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
    //private InputAction.CallbackContext contextClickToMove;
    private Vector2 MousePosition;

    private InputAction LeftMouseClick;
    private InputAction LeftMouseHold;
    //private Quaternion deltaRotation;
    //private Rigidbody rb;
    private void Awake()
    {
        //mouse = new Mouse();
        //mouse = InputSystem.GetDevice<Mouse>();

        inputMaster = new InputMaster();
        LeftMouseClick = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton", interactions: "press");
        LeftMouseHold = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton", interactions: "hold(duration=0.8)");
       
        inputMaster.Player.MousePosition.performed += context => MousePosition = context.ReadValue<Vector2>();

        LeftMouseHold.performed += context =>
        {
            VirtualInputManager.Instance.MouseLeftHold = true;
        };
        LeftMouseHold.canceled += context =>
        {
            VirtualInputManager.Instance.MouseLeftHold = false;
        };
        //deltaRotation = Quaternion.identity;
        //rb = characterControl.transform.GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log("Click " + LeftMouseClick.triggered);
        //Debug.Log("Hold " + LeftMouseHold.triggered);
        VirtualInputManager.Instance.MousePosition = MousePosition;//mouse.position.ReadValue();
        if (LeftMouseClick.triggered) //Mouse.current.leftButton.wasPressedThisFrame
        {
            Ray ray = Camera.main.ScreenPointToRay(MousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Ground")))
            {
                //Debug.Log(hit.point);
                //VirtualInputManager.Instance.Attack = true;
                VirtualInputManager.Instance.ClickPosition = hit.point;
                VirtualInputManager.Instance.MouseLeftClicked = true;
            }
        }
        else
        {
            VirtualInputManager.Instance.MouseLeftClicked = false;
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
    private void OnEnable()
    {
        inputMaster.Player.Enable();
        LeftMouseClick.Enable();
        LeftMouseHold.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Player.Disable();
        LeftMouseClick.Disable();
        LeftMouseHold.Disable();
    }

    //private void OnDisable() => inputMaster.Player.Disable();
}
