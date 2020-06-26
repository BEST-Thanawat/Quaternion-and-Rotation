using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (VirtualInputManager.Instance.PressedW || VirtualInputManager.Instance.PressedS)
        {
            //animator.SetFloat(TransitionParameter.Forward.ToString(), 1f, 0.1f, Time.deltaTime);
            var targetInput = new Vector3(VirtualInputManager.Instance.KeyboardPressedValue.x, 0, VirtualInputManager.Instance.KeyboardPressedValue.y);
            animator.SetFloat(TransitionParameter.Forward.ToString(), targetInput.y, 0.1f, Time.deltaTime);
            Debug.Log(targetInput);
            //dirKeyBoardToMove = Vector3.Lerp(dirKeyBoardToMove, targetInput, Time.deltaTime * speed);

            //movement.Set(dirKeyBoardToMove.x, 0f, dirKeyBoardToMove.z);
            //movement = movement * speed * Time.deltaTime;
            //if (movement != Vector3.zero) rigidbody.MovePosition(transform.position + movement);
        }
        else
        {
            animator.SetFloat(TransitionParameter.Forward.ToString(), 0f, 0f, Time.deltaTime);
        }

        //if (VirtualInputManager.Instance.PressedW)
        //{
        //    animator.SetBool(TransitionParameter.Move.ToString(), true);
        //}

        //if (VirtualInputManager.Instance.PressedA)
        //{
        //    animator.SetFloat(TransitionParameter.Turn.ToString(), -1f);
        //}

        //if (VirtualInputManager.Instance.PressedS)
        //{
        //    animator.SetBool(TransitionParameter.Move.ToString(), true);
        //}

        //if (VirtualInputManager.Instance.PressedD)
        //{
        //    animator.SetFloat(TransitionParameter.Turn.ToString(), 1f);
        //}
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
