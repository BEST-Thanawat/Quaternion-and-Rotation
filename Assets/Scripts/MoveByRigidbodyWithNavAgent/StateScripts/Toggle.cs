using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "MyGame/AbilityData/Toggle")]
public class Toggle : StateData
{
    [Range(0.01f, 1f)]
    public float CheckTime;
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl characterControl = characterState.GetCharacterControl(animator);

        if (stateInfo.normalizedTime >= CheckTime)
        {
            if (characterControl.PressedC)
            {
                animator.SetBool(TransitionParameter.ForceTransition.ToString(), true);
            }
        }
        
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(TransitionParameter.Crouch.ToString(), false);
        animator.SetBool(TransitionParameter.ForceTransition.ToString(), false);
    }
}
