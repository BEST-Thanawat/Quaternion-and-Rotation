using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Networking;

public enum TransitionParameter
{
    Turn,
    Forward,
    ForceTransition,
    Grounded,
    WalkForward,
    WalkBackward,
    StrafeLeft,
    StrafeRight,
    Jump,
    Run,
    Crouch
}
public class CharacterControl : MonoBehaviour
{
    public Animator CharacterAnimator;
    public bool PressedW;
    public bool PressedA;
    public bool PressedS;
    public bool PressedD;
    public bool PressedC;
    public bool PressedLShift;
    public Vector2 MousePositionValue;
    public Vector2 KeyboardPressedValue;
    public Vector2 RotateValue;
    public bool Jump;
    public GameObject ColliderEdgePrefab;
    public bool CrouchToggle = false;

    [Header("Ground Check")]
    public List<GameObject> BottomSpheres = new List<GameObject>();
    public List<GameObject> FrontSpheres = new List<GameObject>();
    public List<GameObject> BackSpheres = new List<GameObject>();
    public List<GameObject> LeftSpheres = new List<GameObject>();
    public List<GameObject> RightSpheres = new List<GameObject>();
    public float GroundCheckOffset = 0.02f;

    [Header("Gravity")]
    public float GravityMultiplier;
    public float PullMultiplier;

    private Rigidbody rigid;
    public Rigidbody RIGID_BODY
    {
        get
        {
            if(rigid == null)
            {
                rigid = GetComponent<Rigidbody>();
            }
            return rigid;
        }
    }

    private void Awake()
    {
        CapsuleCollider capsule = GetComponent<CapsuleCollider>();

        float bottom = capsule.bounds.center.y - capsule.bounds.extents.y;
        float top = capsule.bounds.center.y + capsule.bounds.extents.y;
        float front = capsule.bounds.center.z + capsule.bounds.extents.z;
        float back = capsule.bounds.center.z - capsule.bounds.extents.z;
        float left = capsule.bounds.center.x - capsule.bounds.extents.x;
        float right = capsule.bounds.center.x + capsule.bounds.extents.x;

        //Create edge sphere
        GameObject bottomFront = CreateEdgeSphere(new Vector3(0f, bottom + GroundCheckOffset, front));
        GameObject bottomBack = CreateEdgeSphere(new Vector3(0f, bottom + GroundCheckOffset, back));
        GameObject topFront = CreateEdgeSphere(new Vector3(0f, top, front));
        GameObject topBack = CreateEdgeSphere(new Vector3(0f, top, back));
        GameObject bottomLeft = CreateEdgeSphere(new Vector3(left, GroundCheckOffset, 0f) + (this.transform.position));
        GameObject bottomRight = CreateEdgeSphere(new Vector3(right, GroundCheckOffset, 0f) + (this.transform.position));
        GameObject topLeft = CreateEdgeSphere(new Vector3(left, top - bottom, 0f) + (this.transform.position));
        GameObject topRight = CreateEdgeSphere(new Vector3(right, top - bottom, 0f) + (this.transform.position));

        //bottomLeft.transform.SetParent(this.transform, true);
        //bottomRight.transform.SetParent(this.transform, true);
        //GameObject topbottom = CreateEdgeSphere(new Vector3(0f, top, bottom));

        bottomFront.transform.parent = this.transform;
        bottomBack.transform.parent = this.transform;
        topFront.transform.parent = this.transform;
        topBack.transform.parent = this.transform;
        bottomLeft.transform.parent = this.transform;
        bottomRight.transform.parent = this.transform;
        topLeft.transform.parent = this.transform;
        topRight.transform.parent = this.transform;

        //bottomLeft.transform.position += (this.transform.position);
        //bottomRight.transform.position += (this.transform.position);
        //topLeft.transform.position += (this.transform.position);
        //topRight.transform.position += (this.transform.position);
        //bottomLeft.transform.localPosition = bottomLeft.transform.position;
        //bottomRight.transform.localPosition = bottomRight.transform.position;


        BottomSpheres.Add(bottomFront);
        BottomSpheres.Add(bottomBack);
        BottomSpheres.Add(bottomLeft);
        BottomSpheres.Add(bottomRight);

        FrontSpheres.Add(bottomFront);
        FrontSpheres.Add(topFront);

        BackSpheres.Add(topBack);
        BackSpheres.Add(bottomBack);

        LeftSpheres.Add(bottomLeft);
        LeftSpheres.Add(topLeft);

        RightSpheres.Add(bottomRight);
        RightSpheres.Add(topRight);

        //Create children spheres
        float horSec = (bottomFront.transform.position - bottomBack.transform.position).magnitude / 3f;
        CreateMiddleSpheres(bottomFront, -this.transform.forward, horSec, 2, BottomSpheres);
        float verSec = (bottomFront.transform.position - topFront.transform.position).magnitude / 4f;
        CreateMiddleSpheres(bottomFront, this.transform.up, verSec, 3, FrontSpheres);
        CreateMiddleSpheres(bottomBack, this.transform.up, verSec, 3, BackSpheres);

        CreateMiddleSpheres(bottomLeft, this.transform.up, verSec, 3, LeftSpheres);
        CreateMiddleSpheres(bottomRight, this.transform.up, verSec, 3, RightSpheres);
    }

    private void FixedUpdate()
    {
        if (RIGID_BODY.velocity.y < 0f)
        {
            RIGID_BODY.velocity += (Vector3.down * GravityMultiplier);
        }

        if (RIGID_BODY.velocity.y > 0f && !Jump)
        {
            RIGID_BODY.velocity += (Vector3.down * PullMultiplier);
        }
    }
    public void CreateMiddleSpheres(GameObject start, Vector3 direction, float sec, int interations, List<GameObject> spheresLlst)
    {
        for (int i = 0; i < interations; i++)
        {
            Vector3 pos = start.transform.position + (direction * sec * (i + 1));
            GameObject newObj = CreateEdgeSphere(pos);
            newObj.transform.parent = this.transform;
            spheresLlst.Add(newObj);
        }
    }
    private GameObject CreateEdgeSphere(Vector3 position)
    {
        GameObject obj = Instantiate(ColliderEdgePrefab, position, Quaternion.identity);
        return obj;
    }
}
