using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "MyGame/AbilityData/GroundDetector")]
public class GroundDetector : StateData
{
    [Range(0.01f, 1f)]
    public float CheckTime;
    public float Distance;
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {
        CharacterControl control = characterState.GetCharacterControl(animator);

        if (stateInfo.normalizedTime >= CheckTime)
        {
            if (IsGrounded(control))
            {
                animator.SetBool(TransitionParameter.Grounded.ToString(), true);
            }
            else
            {
                animator.SetBool(TransitionParameter.Grounded.ToString(), false);
            }
        }
    }
    public override void OnMove(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
    {

    }

    bool IsGrounded(CharacterControl control)
    {
        //Debug.Log(Mathf.Approximately(control.RIGID_BODY.velocity.y, 0));
        if (Mathf.Approximately(control.RIGID_BODY.velocity.y, 0.0f))
        {
            return true;
        }
        //if(control.RIGID_BODY.velocity.y >= -0.000001f && control.RIGID_BODY.velocity.y <= 0.0f)
        //{
        //    return true;
        //}

        //Debug.Log(control.RIGID_BODY.velocity.y);
        //if (control.RIGID_BODY.velocity.y < 0.000001f)
        //{
            foreach (GameObject o in control.BottomSpheres)
            {
                Debug.DrawRay(o.transform.position, Vector3.down * Distance, Color.yellow);
                RaycastHit hit;
                if (Physics.Raycast(o.transform.position, Vector3.down, out hit, Distance))
                {
                    //Ignore own player part.
                    if (!control.RagdollParts.Contains(hit.collider))
                    {
                        return true;
                    }
                }
            }
        //}

        return false;
    }
}