﻿using System.Collections;
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
        characterControl.MousePositionValue = VirtualInputManager.Instance.MousePositionValue;
        characterControl.RotateValue = VirtualInputManager.Instance.RotateValue;
        
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
    }
}