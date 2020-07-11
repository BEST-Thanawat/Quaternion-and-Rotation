using System;
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
    public bool PressedC;
    public bool PressedLShift;
    //public Vector2 MousePositionVector2;
    public Vector3 MousePositionVector3;
    public Vector2 KeyboardPressedValue;
    //public Vector2 RotateValue;
    public bool Jump;

    public bool Attack;
    public Vector3 ClickPosition;
    public bool MouseLeftClicked;
    public bool MouseLeftHold;
    public bool MouseRightClicked;
    public bool MouseRightHold;
    //public bool MustMove;

    public Vector3 Velocity;
}
