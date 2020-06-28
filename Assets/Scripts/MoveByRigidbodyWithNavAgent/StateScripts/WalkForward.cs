using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "MyGame/AbilityData/WalkForward")]
public class WalkForward : StateData
{
    private float forwardLerp;
    private float turnLerp;
    private static float timeLerpForward = 0.0f;
    private static float timeLerpTurn = 0.0f;
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl characterControl = characterState.GetCharacterControl(animator);

        if (!characterControl.PressedW)
        {
            animator.SetBool(TransitionParameter.WalkForward.ToString(), false);
            return;
        }

        if (characterControl.PressedW)
        {
            animator.SetBool(TransitionParameter.WalkForward.ToString(), true);
        }

        if (characterControl.Jump)
        {
            animator.SetBool(TransitionParameter.Jump.ToString(), true);
        }

        if (characterControl.PressedLShift)
        {
            animator.SetBool(TransitionParameter.Run.ToString(), true);
        }
        //var targetInput = new Vector3(characterControl.KeyboardPressedValue.x, 0, characterControl.KeyboardPressedValue.y);
        //if (characterControl.PressedLShift)
        //{
        //    targetInput.x *= 1f;
        //    targetInput.z *= 1f;
        //}
        //else
        //{
        //    targetInput.x *= 0.5f;
        //    targetInput.z *= 0.5f;
        //}

        //if (characterControl.PressedW || characterControl.PressedS)
        //{
        //    animator.SetBool(TransitionParameter.Forward.ToString(), true);
        //}
        //else
        //{
        //    animator.SetBool(TransitionParameter.Forward.ToString(), false);
        //    animator.SetBool(TransitionParameter.ForceTransition.ToString(), true);
        //}

        //if (characterControl.PressedW)
        //{
        //    if (characterControl.Jump)
        //    {
        //        animator.SetBool(TransitionParameter.Jump.ToString(), true);
        //    }
        //}

        //if (characterControl.PressedW || characterControl.PressedS || characterControl.PressedA || characterControl.PressedD)
        //{
        //    animator.SetBool(TransitionParameter.Move.ToString(), true);
        //}
        //else
        //{
        //    animator.SetBool(TransitionParameter.Move.ToString(), false);
        //}
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        //animator.SetBool(TransitionParameter.ForceTransition.ToString(), false);
    }
}
