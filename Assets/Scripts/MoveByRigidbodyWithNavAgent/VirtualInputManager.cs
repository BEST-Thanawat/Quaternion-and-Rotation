using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VirtualInputManager : Singleton<VirtualInputManager>
{
    public bool PressedW;
    public bool PressedA;
    public bool PressedS;
    public bool PressedD;
    public bool PressedLShift;
    public Vector2 MousePositionValue;
    public Vector2 KeyboardPressedValue;

    private Keyboard keyboard;
    private Mouse mouse;
    private InputMaster inputMaster;
    private InputAction.CallbackContext contextClickToMove;
    private InputAction.CallbackContext contextKeyboardPressed;
    private void Awake()
    {
        keyboard = new Keyboard();
        keyboard = InputSystem.GetDevice<Keyboard>();

        mouse = new Mouse();
        mouse = InputSystem.GetDevice<Mouse>();

        inputMaster = new InputMaster();
        inputMaster.Player.ClickToMove.performed += context => contextClickToMove = context;
        inputMaster.Player.Movement.performed += context => contextKeyboardPressed = context;
    }
    private void Update()
    {
        KeyboardPressedValue = contextKeyboardPressed.ReadValue<Vector2>();
        MousePositionValue = mouse.position.ReadValue();
        
        if (keyboard.wKey.isPressed)
        {
            PressedW = true;
        }
        else
        {
            PressedW = false;
        }

        if (keyboard.aKey.isPressed)
        {
            PressedA = true;
        }
        else
        {
            PressedA = false;
        }

        if (keyboard.sKey.isPressed)
        {
            PressedS = true;
        }
        else
        {
            PressedS = false;
        }

        if (keyboard.dKey.isPressed)
        {
            PressedD = true;
        }
        else
        {
            PressedD = false;
        }

        if (keyboard.leftShiftKey.isPressed)
        {
            PressedLShift = true;
        }
        else
        {
            PressedLShift = false;
        }
    }

    private void OnEnable() => inputMaster.Player.Enable();
    private void OnDisable() => inputMaster.Player.Disable();
}
