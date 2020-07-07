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

    public bool IsGrounded(CharacterControl control, float distance, out RaycastHit hit, LayerMask layerMask)
    {
        hit = new RaycastHit();
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

        if (layerMask == -1)
        {
            foreach (GameObject o in control.BottomSpheres)
            {
                Debug.DrawRay(o.transform.position, Vector3.down * distance, Color.yellow);
                //RaycastHit hit;
                if (Physics.Raycast(o.transform.position, Vector3.down, out hit, distance))
                {
                    //Ignore own player part.
                    if (!control.RagdollParts.Contains(hit.collider))
                    {
                        return true;
                    }
                }
            }
        }
        else
        {
            foreach (GameObject o in control.BottomSpheres)
            {
                Debug.DrawRay(o.transform.position, Vector3.down * distance, Color.yellow);
                //RaycastHit hit;
                if (Physics.Raycast(o.transform.position, Vector3.down, out hit, distance, layerMask))
                {
                    //Ignore own player part.
                    if (!control.RagdollParts.Contains(hit.collider))
                    {
                        return true;
                    }
                }
            }
        }
        
        //}

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
