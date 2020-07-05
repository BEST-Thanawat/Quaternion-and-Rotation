using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "MyGame/AbilityData/Jump")]
public class Jump : StateData
{
    public float JumpForce;
    public AnimationCurve Gravity;
    public AnimationCurve Pull;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        characterState.GetCharacterControl(animator).RIGID_BODY.AddForce(Vector3.up * JumpForce);
        animator.SetBool(TransitionParameter.Grounded.ToString(), false);
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl characterControl = characterState.GetCharacterControl(animator);

        characterControl.GravityMultiplier = Gravity.Evaluate(stateInfo.normalizedTime);
        characterControl.PullMultiplier = Pull.Evaluate(stateInfo.normalizedTime);
    }
    /*public override void OnMove(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }*/

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl characterControl = characterState.GetCharacterControl(animator);

        animator.SetBool(TransitionParameter.Jump.ToString(), false);
        characterControl.GravityMultiplier = 0;
        characterControl.PullMultiplier = 0;
    }
}
