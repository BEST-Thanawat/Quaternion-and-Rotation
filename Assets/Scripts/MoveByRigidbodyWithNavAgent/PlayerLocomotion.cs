using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : CharacterStateBase
{
    private float forwardLerp;
    private float turnLerp;
    private static float timeLerpForward = 0.0f;
    private static float timeLerpTurn = 0.0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var targetInput = new Vector3(VirtualInputManager.Instance.KeyboardPressedValue.x, 0, VirtualInputManager.Instance.KeyboardPressedValue.y);
        if (VirtualInputManager.Instance.PressedLShift)
        {
            targetInput.x *= 1f;
            targetInput.z *= 1f;
        }
        else
        {
            targetInput.x *= 0.5f;
            targetInput.z *= 0.5f;
        }

        if (VirtualInputManager.Instance.PressedW || VirtualInputManager.Instance.PressedS)
        {
            //animator.SetFloat(TransitionParameter.Forward.ToString(), 1f, 0.1f, Time.deltaTime);
            forwardLerp = Mathf.Lerp(forwardLerp, targetInput.z, timeLerpForward);
            timeLerpForward += 0.1f * Time.deltaTime;
            animator.SetFloat(TransitionParameter.Forward.ToString(), forwardLerp, 0.1f, Time.deltaTime);
        }
        else
        {
            animator.SetFloat(TransitionParameter.Forward.ToString(), 0, 0.1f, Time.deltaTime);
        }

        if (VirtualInputManager.Instance.PressedA || VirtualInputManager.Instance.PressedD)
        {
            //animator.SetFloat(TransitionParameter.Forward.ToString(), 1f, 0.1f, Time.deltaTime);
            turnLerp = Mathf.Lerp(turnLerp, targetInput.x, timeLerpTurn);
            timeLerpTurn += 0.1f * Time.deltaTime;
            animator.SetFloat(TransitionParameter.Turn.ToString(), turnLerp, 0.1f, Time.deltaTime);
        }
        else
        {
            animator.SetFloat(TransitionParameter.Turn.ToString(), 0, 0.1f, Time.deltaTime);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
