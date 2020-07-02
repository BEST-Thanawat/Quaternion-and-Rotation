using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "MyGame/AbilityData/Run")]
public class Run : StateData
{
    public float BlockDistance;
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl characterControl = characterState.GetCharacterControl(animator);

        if (!characterControl.PressedLShift)
        {
            animator.SetBool(TransitionParameter.Run.ToString(), false);
            return;
        }

        if (characterControl.IsArrived)
        {
            animator.SetBool(TransitionParameter.Run.ToString(), false);
        }

        if (characterControl.PressedLShift && !characterControl.IsArrived)
        {
            if (Utility.Instance.CheckFront(characterControl, BlockDistance))
            {
                animator.SetBool(TransitionParameter.Run.ToString(), true);
            }
        }

        if (characterControl.Jump)
        {
            animator.SetBool(TransitionParameter.Jump.ToString(), true);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
