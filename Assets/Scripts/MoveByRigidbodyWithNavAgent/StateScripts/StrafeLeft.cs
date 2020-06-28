﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "MyGame/AbilityData/StrafeLeft")]
public class StrafeLeft : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl characterControl = characterState.GetCharacterControl(animator);

        if (!characterControl.PressedA)
        {
            animator.SetBool(TransitionParameter.StrafeLeft.ToString(), false);
            return;
        }

        if (characterControl.PressedA)
        {
            animator.SetBool(TransitionParameter.StrafeLeft.ToString(), true);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
