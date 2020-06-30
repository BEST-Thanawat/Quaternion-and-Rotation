using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInput : MonoBehaviour
{
    private Keyboard keyboard;
    
    private InputMaster inputMaster;
    private InputAction.CallbackContext contextKeyboardPressed;
    private void Awake()
    {
        keyboard = new Keyboard();
        keyboard = InputSystem.GetDevice<Keyboard>();

        inputMaster = new InputMaster();
        inputMaster.Player.Movement.performed += context => contextKeyboardPressed = context;
    }
    private void Update()
    {
        VirtualInputManager.Instance.KeyboardPressedValue = contextKeyboardPressed.ReadValue<Vector2>();

        if (keyboard.wKey.isPressed)
        {
            VirtualInputManager.Instance.PressedW = true;
            VirtualInputManager.Instance.KeyboardPressedValue.y = OptimizeValue(VirtualInputManager.Instance.KeyboardPressedValue.y);
        }
        else
        {
            VirtualInputManager.Instance.PressedW = false;
        }

        if (keyboard.aKey.isPressed)
        {
            VirtualInputManager.Instance.PressedA = true;
            VirtualInputManager.Instance.KeyboardPressedValue.x = OptimizeValue(VirtualInputManager.Instance.KeyboardPressedValue.x);
        }
        else
        {
            VirtualInputManager.Instance.PressedA = false;
        }

        if (keyboard.sKey.isPressed)
        {
            VirtualInputManager.Instance.PressedS = true;
            VirtualInputManager.Instance.KeyboardPressedValue.y = OptimizeValue(VirtualInputManager.Instance.KeyboardPressedValue.y);
        }
        else
        {
            VirtualInputManager.Instance.PressedS = false;
        }

        if (keyboard.dKey.isPressed)
        {
            VirtualInputManager.Instance.PressedD = true;
            VirtualInputManager.Instance.KeyboardPressedValue.x = OptimizeValue(VirtualInputManager.Instance.KeyboardPressedValue.x);
        }
        else
        {
            VirtualInputManager.Instance.PressedD = false;
        }

        if (keyboard.cKey.isPressed)
        {
            VirtualInputManager.Instance.PressedC = true;
        }
        else
        {
            VirtualInputManager.Instance.PressedC = false;
        }

        if (keyboard.leftShiftKey.isPressed)
        {
            VirtualInputManager.Instance.PressedLShift = true;
        }
        else
        {
            VirtualInputManager.Instance.PressedLShift = false;
        }

        if (keyboard.spaceKey.isPressed)
        {
            VirtualInputManager.Instance.Jump = true;
        }
        else
        {
            VirtualInputManager.Instance.Jump = false;
        }
    }
    private float OptimizeValue(float value)
    {
        if (value < 0)
        {
            value = value < -0.7f ? -1f : value;
        }
        else
        {
            value = value > 0.7f ? 1f : value;
        }
        return value;
    }

    private void OnEnable() => inputMaster.Player.Enable();
    private void OnDisable() => inputMaster.Player.Disable();
}
