using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "MyGame/AbilityData/WalkBackward")]
public class WalkBackward : StateData
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

        if (!characterControl.PressedS)
        {
            animator.SetBool(TransitionParameter.WalkBackward.ToString(), false);
            return;
        }

        if (characterControl.PressedS)
        {
            animator.SetBool(TransitionParameter.WalkBackward.ToString(), true);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
}
