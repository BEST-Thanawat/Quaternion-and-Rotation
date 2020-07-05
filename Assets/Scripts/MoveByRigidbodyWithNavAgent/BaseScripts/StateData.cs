using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class StateData : ScriptableObject
{
    public abstract void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo);
    public abstract void UpdateAbility(CharacterState characterStateBase, Animator animator, AnimatorStateInfo stateInfo);
    //public abstract void OnMove(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo);
    public abstract void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo);
}
