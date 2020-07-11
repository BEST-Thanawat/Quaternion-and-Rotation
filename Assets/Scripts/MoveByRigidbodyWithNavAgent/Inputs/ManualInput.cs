using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualInput : MonoBehaviour
{
    private CharacterControl characterControl;
    private void Awake()
    {
        characterControl = this.gameObject.GetComponent<CharacterControl>();
    }

    private void Update()
    {
        characterControl.KeyboardPressedValue = VirtualInputManager.Instance.KeyboardPressedValue;
        //characterControl.MousePositionVector2 = VirtualInputManager.Instance.MousePositionVector2;
        characterControl.MousePositionVector3 = VirtualInputManager.Instance.MousePositionVector3;
        characterControl.Velocity = VirtualInputManager.Instance.Velocity;

        if (VirtualInputManager.Instance.MouseLeftClicked)
        {
            characterControl.MouseLeftClicked = true;
            characterControl.ClickPosition = VirtualInputManager.Instance.ClickPosition;
        }
        else
        {
            characterControl.MouseLeftClicked = false;
        }

        if (VirtualInputManager.Instance.MouseLeftHold)
        {
            characterControl.MouseLeftHold = true;
        }
        else
        {
            characterControl.MouseLeftHold = false;
        }

        if (VirtualInputManager.Instance.MouseRightClicked)
        {
            characterControl.MouseRightClicked = true;
            characterControl.ClickPosition = VirtualInputManager.Instance.ClickPosition;
        }
        else
        {
            characterControl.MouseRightClicked = false;
        }

        if (VirtualInputManager.Instance.MouseRightHold)
        {
            characterControl.MouseRightHold = true;
        }
        else
        {
            characterControl.MouseRightHold = false;
        }

        if (VirtualInputManager.Instance.PressedW)
        {
            characterControl.PressedW = true;
        }
        else
        {
            characterControl.PressedW = false;
        }

        if (VirtualInputManager.Instance.PressedS)
        {
            characterControl.PressedS = true;
        }
        else
        {
            characterControl.PressedS = false;
        }

        if (VirtualInputManager.Instance.PressedA)
        {
            characterControl.PressedA = true;
        }
        else
        {
            characterControl.PressedA = false;
        }

        if (VirtualInputManager.Instance.PressedD)
        {
            characterControl.PressedD = true;
        }
        else
        {
            characterControl.PressedD = false;
        }

        if (VirtualInputManager.Instance.PressedC)
        {
            characterControl.PressedC = true;
        }
        else
        {
            characterControl.PressedC = false;
        }

        if (VirtualInputManager.Instance.PressedLShift)
        {
            characterControl.PressedLShift = true;
        }
        else
        {
            characterControl.PressedLShift = false;
        }

        if (VirtualInputManager.Instance.Jump)
        {
            characterControl.Jump = true;
        }
        else
        {
            characterControl.Jump = false;
        }

        if (VirtualInputManager.Instance.Attack)
        {
            characterControl.Attack = true;
        }
        else
        {
            characterControl.Attack = false;
        }
    }
}
