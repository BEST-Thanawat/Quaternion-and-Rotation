using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    private CharacterControl owner;

    private void Awake()
    {
        owner = this.GetComponentInParent<CharacterControl>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        //This is ourself bodypart so do nothing.
        if (owner.RagdollParts.Contains(collider)) return;

        CharacterControl attacker = collider.transform.root.GetComponent<CharacterControl>();
        //This is not other character so do nothing. It is something else.
        if (attacker == null) return;

        if (collider.gameObject == attacker.gameObject) return;

        //This is bodypart of other player.
        if (!owner.CollidingParts.Contains(collider))
        {
            owner.CollidingParts.Add(collider);
        }
    }
    private void OnTriggerExit(Collider attacker)
    {
        if (owner.CollidingParts.Contains(attacker))
        {
            owner.CollidingParts.Remove(attacker);
        }
    }
}
