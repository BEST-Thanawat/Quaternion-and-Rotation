using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Animations;

public class CharacterState : StateMachineBehaviour
{
    public List<StateData> ListAbilityData = new List<StateData>();

    private CharacterControl characterControl;

    public void UpdateAll(CharacterState characterStateBase, Animator animator)
    {
        foreach (StateData d in ListAbilityData)
        {
            d.UpdateAbility(characterStateBase, animator);
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UpdateAll(this, animator);
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
