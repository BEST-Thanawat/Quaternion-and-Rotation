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
        characterControl.MousePosition = VirtualInputManager.Instance.MousePosition;
        characterControl.Velocity = VirtualInputManager.Instance.Velocity;

        //if (VirtualInputManager.Instance.MustMove)
        //{
        //    characterControl.MustMove = true;
        //}
        //else
        //{
        //    characterControl.MustMove = false;
        //}

        if (VirtualInputManager.Instance.MouseClicked)
        {
            characterControl.MouseClicked = true;
            characterControl.ClickPosition = VirtualInputManager.Instance.ClickPosition;
        }
        else
        {
            characterControl.MouseClicked = false;
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
            //characterControl.ClickPosition = VirtualInputManager.Instance.ClickPosition;
        }
        else
        {
            characterControl.Attack = false;
            //characterControl.ClickPosition = Vector3.zero;
        }
    }
}
