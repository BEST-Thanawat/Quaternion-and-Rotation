using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : Singleton<Utility>
{
    public bool CheckFront(CharacterControl control, float BlockDistance)
    {
        foreach (GameObject o in control.FrontSpheres)
        {
            Debug.DrawRay(o.transform.position, control.transform.forward * BlockDistance, Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(o.transform.position, control.transform.forward, out hit, BlockDistance))
            {
                if (!control.RagdollParts.Contains(hit.collider))
                {
                    if (!IsBodyPart(hit.collider))
                    {
                        //Debug.Log(true);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public bool CheckLeft(CharacterControl control, float BlockDistance)
    {
        foreach (GameObject o in control.LeftSpheres)
        {
            Debug.DrawRay(o.transform.position, -control.transform.right * BlockDistance, Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(o.transform.position, -control.transform.right, out hit, BlockDistance))
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
    public bool CheckRight(CharacterControl control, float BlockDistance)
    {
        foreach (GameObject o in control.RightSpheres)
        {
            Debug.DrawRay(o.transform.position, control.transform.right * BlockDistance, Color.yellow);
            RaycastHit hit;
            if (Physics.Raycast(o.transform.position, control.transform.right, out hit, BlockDistance))
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
    public bool CheckBack(CharacterControl control, float BlockDistance)
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
                        //Debug.Log(true);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public bool IsBodyPart(Collider collider)
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
