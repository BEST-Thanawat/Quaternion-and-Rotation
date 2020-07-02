using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GeneralBodyPart
{
    Upper,
    Lower,
    Arm,
    Leg,
}
public class TriggerDetector : MonoBehaviour
{
    public GeneralBodyPart GeneralBodyPart;

    public List<Collider> CollidingParts = new List<Collider>();
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
        if (!CollidingParts.Contains(collider))
        {
            CollidingParts.Add(collider);
        }
    }
    private void OnTriggerExit(Collider attacker)
    {
        if (CollidingParts.Contains(attacker))
        {
            CollidingParts.Remove(attacker);
        }
    }
}
