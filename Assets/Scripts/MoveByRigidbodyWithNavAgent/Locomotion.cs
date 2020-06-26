using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "MyGame/AbilityData/Locomotion")]
public class Locomotion : StateData
{
    //public float Speed;

    private float forwardLerp;
    private float turnLerp;
    private static float timeLerpForward = 0.0f;
    private static float timeLerpTurn = 0.0f;
    public override void UpdateAbility(CharacterState characterStateBase, Animator animator)
    {
        CharacterControl c = characterStateBase.GetCharacterControl(animator);

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
}
