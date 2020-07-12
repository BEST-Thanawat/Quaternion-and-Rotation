using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "MyGame/AbilityData/Idle")]
public class Idle : StateData
{
    //private float forwardLerp;
    //private float turnLerp;
    //private static float timeLerpForward = 0.0f;
    //private static float timeLerpTurn = 0.0f;

    //[Range(0.01f, 1f)]
    //public float CheckTime;
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(TransitionParameter.Jump.ToString(), false);
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl characterControl = characterState.GetCharacterControl(animator);

        if (characterControl.PressedW || !characterControl.IsArrived)
        {
            animator.SetBool(TransitionParameter.WalkForward.ToString(), true);
        }
        if (characterControl.PressedS)
        {
            animator.SetBool(TransitionParameter.WalkBackward.ToString(), true);
        }
        if (characterControl.PressedA)
        {
            animator.SetBool(TransitionParameter.StrafeLeft.ToString(), true);
        }
        if (characterControl.PressedD)
        {
            animator.SetBool(TransitionParameter.StrafeRight.ToString(), true);
        }
        if (characterControl.Jump)
        {
            animator.SetBool(TransitionParameter.Jump.ToString(), true);
        }

        if (characterControl.PressedC)
        {
            animator.SetBool(TransitionParameter.Crouch.ToString(), true);
        }

        if (characterControl.Attack)
        {
            animator.SetBool(TransitionParameter.Attack.ToString(), true);
        }
        else
        {
            animator.SetBool(TransitionParameter.Attack.ToString(), false);
        }

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
        //    //animator.SetFloat(TransitionParameter.Forward.ToString(), 1f, 0.1f, Time.deltaTime);
        //    forwardLerp = Mathf.Lerp(forwardLerp, targetInput.z, timeLerpForward);
        //    timeLerpForward += 0.1f * Time.deltaTime;
        //    animator.SetFloat(TransitionParameter.Forward.ToString(), forwardLerp, 0.1f, Time.deltaTime);
        //}
        //else
        //{
        //    animator.SetFloat(TransitionParameter.Forward.ToString(), 0, 0.1f, Time.deltaTime);
        //    animator.SetBool(TransitionParameter.ForceTransition.ToString(), true);
        //}

        //if (characterControl.PressedW || characterControl.PressedS || characterControl.PressedA || characterControl.PressedD)
        //{
        //    animator.SetBool(TransitionParameter.Move.ToString(), true);
        //}
    }
    //public override void OnMove(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    //{

    //}

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
