using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Animations;
using UnityEngine.PlayerLoop;

public class CharacterState : StateMachineBehaviour
{
    public List<StateData> ListAbilityData = new List<StateData>();

    private CharacterControl characterControl;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (StateData d in ListAbilityData)
        {
            d.OnEnter(this, animator, stateInfo);
        }
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UpdateAll(this, animator, stateInfo);
    }
    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (StateData d in ListAbilityData)
        {
            d.OnMove(this, animator, stateInfo);
        }
        //base.OnStateMove(animator, stateInfo, layerIndex);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (StateData d in ListAbilityData)
        {
            d.OnExit(this, animator, stateInfo);
        }
    }

    public void UpdateAll(CharacterState characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
    {
        foreach (StateData d in ListAbilityData)
        {
            d.UpdateAbility(characterStateBase, animator, stateInfo);
        }
    }

    public CharacterControl GetCharacterControl(Animator animator)
    {
        if(characterControl == null)
        {
            characterControl = animator.GetComponent<CharacterControl>();
        }
        return characterControl;
    }
}
