using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "MyGame/AbilityData/CrouchForward")]
public class CrouchForward : StateData
{
    public float BlockDistance;
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
    }
    public override void OnMove(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        
    }
}
