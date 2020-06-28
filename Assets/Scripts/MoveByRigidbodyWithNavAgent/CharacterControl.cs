using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public enum TransitionParameter
{
    Turn,
    Forward,
    Jump,
    ForceTransition,
    Grounded
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

    public List<GameObject> BottomSpheres = new List<GameObject>();
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

        GameObject bottomFront = CreateEdgeSphere(new Vector3(0f, bottom, front));
        GameObject bottomBack = CreateEdgeSphere(new Vector3(0f, bottom, back));

        bottomFront.transform.parent = this.transform;
        bottomBack.transform.parent = this.transform;

        BottomSpheres.Add(bottomFront);
        BottomSpheres.Add(bottomBack);

        float sec = (bottomFront.transform.position - bottomBack.transform.position).magnitude / 5f;

        for (int i = 0; i < 4; i++)
        {
            Vector3 pos = bottomBack.transform.position + (Vector3.forward * sec * (i + 1));
            GameObject newObj = CreateEdgeSphere(pos);
            newObj.transform.parent = this.transform;
            BottomSpheres.Add(newObj);
        }
    }

    private GameObject CreateEdgeSphere(Vector3 position)
    {
        GameObject obj = Instantiate(ColliderEdgePrefab, position, Quaternion.identity);
        return obj;
    }
}
