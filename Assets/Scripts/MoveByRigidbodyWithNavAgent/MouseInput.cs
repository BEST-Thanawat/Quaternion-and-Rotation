using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour
{
    [SerializeField] public float MovingTurnSpeed = 360;
    [SerializeField] public float StationaryTurnSpeed = 180;
    [SerializeField] public float ForwardAmount = 1f;

    private CharacterControl characterControl;
    private Mouse mouse;
    private InputMaster inputMaster;
    private InputAction.CallbackContext contextClickToMove;
    private Vector2 rotateValue;
    private Quaternion deltaRotation;
    private Rigidbody rb;
    private void Awake()
    {
        characterControl = this.gameObject.GetComponent<CharacterControl>();

        mouse = new Mouse();
        mouse = InputSystem.GetDevice<Mouse>();

        inputMaster = new InputMaster();
        inputMaster.Player.ClickToMove.performed += context => contextClickToMove = context;
        inputMaster.Player.Turn.performed += context => rotateValue = context.ReadValue<Vector2>();

        deltaRotation = Quaternion.identity;
        rb = characterControl.transform.GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        VirtualInputManager.Instance.MousePositionValue = mouse.position.ReadValue();
        VirtualInputManager.Instance.RotateValue = rotateValue;
    }
    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(characterControl.RotateValue);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            Vector3 move = characterControl.transform.InverseTransformPoint(hit.point);
            float m_TurnAmount = Mathf.Atan2(move.x, move.z);
            float turnSpeed = Mathf.Lerp(StationaryTurnSpeed, MovingTurnSpeed, ForwardAmount);
            deltaRotation = Quaternion.Euler(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
        }
        else
        {
            deltaRotation = Quaternion.identity;
        }

        //Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 30, 0) * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
    private void OnEnable() => inputMaster.Player.Enable();
    private void OnDisable() => inputMaster.Player.Disable();
}
