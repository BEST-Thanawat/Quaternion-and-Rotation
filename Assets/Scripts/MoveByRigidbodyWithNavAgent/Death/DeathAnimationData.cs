using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ScriptableObject", menuName = "MyGame/Death/DeathAnimationData")]
public class DeathAnimationData : ScriptableObject
{
    public List<GeneralBodyPart> GeneralBodyParts = new List<GeneralBodyPart>();
    public RuntimeAnimatorController Animator;
    public bool IsFacingAttacker;
}
