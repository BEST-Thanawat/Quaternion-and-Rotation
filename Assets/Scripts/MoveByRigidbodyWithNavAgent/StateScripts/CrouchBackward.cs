using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "MyGame/AbilityData/CrouchBackward")]
public class CrouchBackward : StateData
{
    public float BlockDistance;
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl characterControl = characterState.GetCharacterControl(animator);

        if (!characterControl.PressedS)
        {
            animator.SetBool(TransitionParameter.WalkBackward.ToString(), false);
            return;
        }

        if (characterControl.PressedW)
        {
            animator.SetBool(TransitionParameter.WalkForward.ToString(), true);
        }

        if (characterControl.PressedA)
        {
            animator.SetBool(TransitionParameter.StrafeLeft.ToString(), true);
        }

        if (characterControl.PressedS)
        {
            if (Utility.Instance.CheckBack(characterControl, BlockDistance))
            {
                animator.SetBool(TransitionParameter.WalkBackward.ToString(), true);
            }
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

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        
    }
}
