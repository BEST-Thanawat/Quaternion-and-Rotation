using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    CharacterControl control;

    private void Awake()
    {
        control = GetComponent<CharacterControl>();
    }

    private void Update()
    {
        if(AttackManager.Instance.CurrentAttacks.Count > 0)
        {
            CheckAttack();
        }
    }

    private void CheckAttack()
    {
        //Loop through attack boardcast AttackManager
        foreach (AttackInfo info in AttackManager.Instance.CurrentAttacks)
        {
            if (info == null) continue;
            if (!info.isRegistered) continue;
            if (info.isFinished) continue;
            if (info.CurrentHits >= info.MaxHits) continue;
            if (info.Attacker == control) continue;

            if (info.MustCollide)
            {
                if (IsCollided(info))
                {
                    TakeDamage(info);
                }
            }
        }
    }

    private void TakeDamage(AttackInfo attackInfo)
    {
        Debug.Log(attackInfo.gameObject.name + " hits: " + this.gameObject.name);
        control.CharacterAnimator.runtimeAnimatorController = attackInfo.AttackAbility.GetDeathAnimator();
        attackInfo.CurrentHits++;

        control.GetComponent<CapsuleCollider>().enabled = false;
        control.RIGID_BODY.useGravity = false;
    }

    private bool IsCollided(AttackInfo attackInfo)
    {
        foreach (Collider collider in control.CollidingParts)
        {
            foreach (string name in attackInfo.ColliderNames)
            {
                //AttackInfo matched with target character
                if(name == collider.gameObject.name)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
