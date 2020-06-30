using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "MyGame/AbilityData/WalkBackward")]
public class WalkBackward : StateData
{
    public float BlockDistance;

    //private float forwardLerp;
    //private float turnLerp;
    //private static float timeLerpForward = 0.0f;
    //private static float timeLerpTurn = 0.0f;
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
            if (CheckBack(characterControl))
            {
                animator.SetBool(TransitionParameter.WalkBackward.ToString(), true);
            }
        }

        if (characterControl.PressedD)
        {
            animator.SetBool(TransitionParameter.StrafeRight.ToString(), true);
        }

        if (characterControl.Jump)
        {
            animator.SetBool(TransitionParameter.Jump.ToString(), true);
        }
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }
    bool CheckBack(CharacterControl control)
    {
        foreach (GameObject o in control.BackSpheres)
        {
            Debug.DrawRay(o.transform.position, -control.transform.forward * BlockDistance, Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(o.transform.position, -control.transform.forward, out hit, BlockDistance))
            {
                if (!control.RagdollParts.Contains(hit.collider))
                {
                    if (!IsBodyPart(hit.collider))
                    {
                        Debug.Log(true);
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private bool IsBodyPart(Collider collider)
    {
        CharacterControl characterControl = collider.transform.root.GetComponent<CharacterControl>();

        if (characterControl == null) return false;
        if (characterControl.gameObject == collider.gameObject) return false;
        if (characterControl.RagdollParts.Contains(collider))
        {
            return true;
        }

        return false;
    }
}
