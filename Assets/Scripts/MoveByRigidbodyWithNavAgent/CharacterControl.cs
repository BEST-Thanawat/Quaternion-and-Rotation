using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TransitionParameter
{
    Turn,
    Forward
}
public class CharacterControl : MonoBehaviour
{
    public float Speed;
    public Animator CharacterAnimator;
}
