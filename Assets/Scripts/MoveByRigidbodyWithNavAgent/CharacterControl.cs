using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TransitionParameter
{
    Turn,
    Forward,
    Jump,
    ForceTransition
}
public class CharacterControl : MonoBehaviour
{
    public float Speed;
    public Animator CharacterAnimator;

    public bool PressedW;
    public bool PressedA;
    public bool PressedS;
    public bool PressedD;
    public bool PressedC;
    public bool PressedLShift;
    public Vector2 MousePositionValue;
    public Vector2 KeyboardPressedValue;
    public Vector2 RotateValue;
    public bool Jump;

    private Rigidbody rigid;
    public Rigidbody RIGID_BODY
    {
        get
        {
            if(rigid == null)
            {
                rigid = GetComponent<Rigidbody>();
            }
            return rigid;
        }
    }
}
