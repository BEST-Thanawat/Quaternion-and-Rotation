﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "MyGame/AbilityData/StrafeRight")]
public class StrafeRight : StateData
{
    public float BlockDistance;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl characterControl = characterState.GetCharacterControl(animator);

        if (!characterControl.PressedD)
        {
            animator.SetBool(TransitionParameter.StrafeRight.ToString(), false);
            return;
        }

        if (characterControl.PressedW)
        {
            animator.SetBool(TransitionParameter.WalkForward.ToString(), true);
        }
        else
        {
            animator.SetBool(TransitionParameter.WalkForward.ToString(), false);
        }

        if (characterControl.PressedA)
        {
            animator.SetBool(TransitionParameter.StrafeLeft.ToString(), true);
        }
        else
        {
            animator.SetBool(TransitionParameter.StrafeLeft.ToString(), false);
        }

        if (characterControl.PressedS)
        {
            animator.SetBool(TransitionParameter.WalkBackward.ToString(), true);
        }
        else
        {
            animator.SetBool(TransitionParameter.WalkBackward.ToString(), false);
        }

        if (characterControl.PressedD)
        {
            if (Utility.Instance.CheckRight(characterControl, BlockDistance))
            {
                animator.SetBool(TransitionParameter.StrafeRight.ToString(), true);
            }
        }
        else
        {
            animator.SetBool(TransitionParameter.StrafeRight.ToString(), false);
        }

        if (characterControl.PressedC)
        {
            animator.SetBool(TransitionParameter.Crouch.ToString(), true);
        }
    }
    public override void OnMove(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
