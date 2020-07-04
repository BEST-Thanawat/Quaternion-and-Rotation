using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "MyGame/AbilityData/WalkForward")]
public class WalkForward : StateData
{
    public float BlockDistance;
    //private Vector3 dirClickToMove;
    //private float speed = 4.5f;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl characterControl = characterState.GetCharacterControl(animator);

        if (!characterControl.PressedW && characterControl.IsArrived)
        {
            animator.SetBool(TransitionParameter.WalkForward.ToString(), false);
            return;
        }

        if (characterControl.PressedW || !characterControl.IsArrived)
        {
            if (Utility.Instance.CheckFront(characterControl, BlockDistance))
            {
                animator.SetBool(TransitionParameter.WalkForward.ToString(), true);
            }
        }
        else if(characterControl.IsArrived)
        {
            animator.SetBool(TransitionParameter.WalkForward.ToString(), false);
        }

        if (characterControl.PressedA)
        {
            animator.SetBool(TransitionParameter.StrafeLeft.ToString(), true);
        }

        if (characterControl.PressedS)
        {
            animator.SetBool(TransitionParameter.WalkBackward.ToString(), true);
        }

        if (characterControl.PressedD)
        {
            animator.SetBool(TransitionParameter.StrafeRight.ToString(), true);
        }

        if (characterControl.PressedC)
        {
            animator.SetBool(TransitionParameter.Crouch.ToString(), true);
        }

        if (characterControl.Jump)
        {
            animator.SetBool(TransitionParameter.Jump.ToString(), true);
        }

        if (characterControl.PressedLShift)
        {
            animator.SetBool(TransitionParameter.Run.ToString(), true);
        }
    }

    public override void OnMove(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl characterControl = characterState.GetCharacterControl(animator);
        characterControl.RIGID_BODY.velocity = characterControl.Velocity;
        //characterControl.RIGID_BODY.MovePosition(characterControl.RIGID_BODY.position + characterControl.Velocity * Time.fixedDeltaTime);

        //CharacterControl characterControl = characterState.GetCharacterControl(animator);
        //if (animator.GetBool(TransitionParameter.Grounded.ToString()) && Time.fixedDeltaTime > 0)
        //{
        //    Vector3 v = (animator.deltaPosition * 1) / Time.fixedDeltaTime;

        //    // we preserve the existing y part of the current velocity.
        //    v.y = characterControl.RIGID_BODY.velocity.y;
        //    characterControl.RIGID_BODY.MovePosition(characterControl.RIGID_BODY.position + v * Time.fixedDeltaTime);
        //    //characterControl.RIGID_BODY.velocity = v;
        //}
    }
    
    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        //animator.SetBool(TransitionParameter.ForceTransition.ToString(), false);
    }
}
