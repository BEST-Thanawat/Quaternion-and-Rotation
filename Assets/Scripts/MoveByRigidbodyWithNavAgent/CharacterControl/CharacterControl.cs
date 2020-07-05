using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;
using UnityEngine.Networking;
using UnityEngine.PlayerLoop;

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
    Crouch,
    Attack
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
    
    public Vector2 KeyboardPressedValue;
    //public Vector2 RotateValue;
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

    public List<Collider> RagdollParts = new List<Collider>();

    [Header("Gravity")]
    public float GravityMultiplier;
    public float PullMultiplier;

    [Header("Attack")]
    public bool Attack;

    [Header("Face To Mouse")]
    public Vector2 MousePosition; //For face to mouse function
    public float RotateSpeedFaceToMouse = 5f;
    //public float MovingTurnSpeed = 360;
    //public float StationaryTurnSpeed = 180;
    //public float ForwardAmount = 1f;

    [Header("Click to Move")]
    public bool MouseClicked;
    public Vector3 ClickPosition; //Is hit.point
    public float StoppingDistance = 0.1f;
    [SerializeField] private float remainingDistance = 0;
    public float RotateSpeed = 2f;
    public bool IsArrived = true;

    [Header("Kinematic Movement")]
    public Vector3 Velocity;
    //private bool MustMove = true;
    //public float turnAmount;

    //private Quaternion deltaRotation;
    //private Vector3 dirClickToMove;
    //private float speed = 4.5f;
    private bool isMoving = false;

    private bool trigggerClickToMove = false;
    private Vector3 relativePosition;
    private Quaternion targetRotation = Quaternion.identity;
    private float rotationTime;

    private float rotationTimeFaceToMouse = 0;
    private Rigidbody rigid;
    private List<TriggerDetector> TriggerDetectors = new List<TriggerDetector>();
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
        //SetRagdollParts();
        SetColliderSpheres();

        //StartCoroutine(Start());

        //deltaRotation = Quaternion.identity;
    }

    //IEnumerator Start()
    //{
    //    yield return new WaitForSeconds(5f);
    //    RIGID_BODY.AddForce(400f * Vector3.up);
    //    yield return new WaitForSeconds(0.5f);
    //    TurnOnRagdoll();
    //}
    private void TurnOnRagdoll()
    {
        RIGID_BODY.useGravity = false;
        RIGID_BODY.velocity = Vector3.zero;
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        CharacterAnimator.enabled = false;
        CharacterAnimator.avatar = null;

        foreach (Collider c in RagdollParts)
        {
            c.isTrigger = false;
            c.attachedRigidbody.velocity = Vector3.zero;
        }
    }

    public List<TriggerDetector> GetAllTrigger()
    {
        if (TriggerDetectors.Count == 0)
        {
            TriggerDetector[] arr = this.gameObject.GetComponentsInChildren<TriggerDetector>();
            foreach (TriggerDetector t in arr)
            {
                TriggerDetectors.Add(t);
            }
        }

        return TriggerDetectors;
    }

    public void SetRagdollParts()
    {
        RagdollParts.Clear();

        Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider c in colliders)
        {
            if (c.gameObject != this.gameObject)
            {
                c.isTrigger = true;
                RagdollParts.Add(c);

                if(c.GetComponent<TriggerDetector>() == null)
                {
                    c.gameObject.AddComponent<TriggerDetector>();
                }                
            }
        }
    }

    private void SetColliderSpheres()
    {
        CapsuleCollider capsule = GetComponent<CapsuleCollider>();

        float bottom = -capsule.bounds.extents.y;//capsule.bounds.center.y - capsule.bounds.extents.y;
        float top = capsule.bounds.extents.y;//capsule.bounds.center.y + capsule.bounds.extents.y;
        float front = capsule.bounds.extents.z;//capsule.bounds.center.z + capsule.bounds.extents.z;
        float back = -capsule.bounds.extents.z;//capsule.bounds.center.z - capsule.bounds.extents.z;
        float left = -capsule.bounds.extents.x;//capsule.bounds.center.x - capsule.bounds.extents.x;
        float right = capsule.bounds.extents.x;//capsule.bounds.center.x + capsule.bounds.extents.x;

        //Create edge sphere
        GameObject bottomFront = CreateEdgeSphere(this.transform.localPosition + new Vector3(0f, GroundCheckOffset, front));
        GameObject bottomBack = CreateEdgeSphere(this.transform.localPosition + new Vector3(0f, GroundCheckOffset, back));
        GameObject topFront = CreateEdgeSphere(this.transform.localPosition + new Vector3(0f, top - bottom, front));
        GameObject topBack = CreateEdgeSphere(this.transform.localPosition + new Vector3(0f, top - bottom, back));
        GameObject bottomLeft = CreateEdgeSphere(this.transform.localPosition + new Vector3(left, GroundCheckOffset, 0f));
        GameObject bottomRight = CreateEdgeSphere(this.transform.localPosition + new Vector3(right, GroundCheckOffset, 0f));
        GameObject topLeft = CreateEdgeSphere(this.transform.localPosition + new Vector3(left, top - bottom, 0f));
        GameObject topRight = CreateEdgeSphere(this.transform.localPosition + new Vector3(right, top - bottom, 0f));

        bottomFront.transform.parent = this.transform;
        bottomBack.transform.parent = this.transform;
        topFront.transform.parent = this.transform;
        topBack.transform.parent = this.transform;
        bottomLeft.transform.parent = this.transform;
        bottomRight.transform.parent = this.transform;
        topLeft.transform.parent = this.transform;
        topRight.transform.parent = this.transform;

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


        float verSec = (bottomFront.transform.position - topFront.transform.position).magnitude / 3f;
        CreateMiddleSpheres(bottomFront, this.transform.up, verSec, 2, FrontSpheres);
        CreateMiddleSpheres(bottomBack, this.transform.up, verSec, 2, BackSpheres);

        CreateMiddleSpheres(bottomLeft, this.transform.up, verSec, 2, LeftSpheres);
        CreateMiddleSpheres(bottomRight, this.transform.up, verSec, 2, RightSpheres);
    }

    
    private void Update()
    {
        //if(trigClickToMove) VirtualInputManager.Instance.PressedW = true;
        //float f = Mathf.Lerp(0f, 100f, Timettt);
        //Timettt += 0.1f * Time.deltaTime;
        //Debug.Log(f);

        //TestCo1();
        //ClickToMove(ClickPosition);
        ClickToMoveUpdate();
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

        ClickToMoveFixedUpdate();

        if (!isMoving)
        {
            FaceToMouse();
        }

        //if (MouseClicked && MustMove)
        //{
        //    MustMove = false;

        //    StartCoroutine(TestCo());
        //    //Move to position

        //}

        //if (MouseClicked)
        //{
        //    MustMove = true;
        //}
    }
    
    private void ClickToMoveUpdate()
    {
        if (MouseClicked)
        {
            relativePosition = ClickPosition - RIGID_BODY.position;
            relativePosition.y = 0f;
            targetRotation = Quaternion.LookRotation(relativePosition);
            //Debug.Log(relativePosition);
            //Debug.Log(targetRotation);
            trigggerClickToMove = true;
            rotationTime = 0;
        }
    }
    private void ClickToMoveFixedUpdate()
    {
        if (trigggerClickToMove)
        {
            isMoving = true;
            rotationTime += Time.deltaTime * RotateSpeed;
            RIGID_BODY.MoveRotation(Quaternion.Lerp(RIGID_BODY.rotation, targetRotation, rotationTime));

            remainingDistance = Vector3.Distance(RIGID_BODY.position, ClickPosition);
            if (!(remainingDistance > StoppingDistance) || (remainingDistance < StoppingDistance))
            {
                trigggerClickToMove = false;
                IsArrived = true;

                StartCoroutine(SetIsMovindToFalse(0.4f));
            }
            else
            {
                IsArrived = false;
            }
        }
    }
    
    private void FaceToMouse()
    {
        if (transform.GetComponent<ManualInput>().enabled)
        {
            //*** Rotate character by mouse position.
            Ray ray = Camera.main.ScreenPointToRay(MousePosition);
            RaycastHit hit;
            Vector3 relativeDirection = Vector3.zero;

            if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Ground")))
            {
                relativeDirection = hit.point - RIGID_BODY.position;
                relativeDirection.y = 0;
                rotationTimeFaceToMouse = 0;
            }

            rotationTimeFaceToMouse += Time.deltaTime * RotateSpeedFaceToMouse;
            if (relativeDirection != Vector3.zero)
            {
                RIGID_BODY.MoveRotation(Quaternion.Lerp(RIGID_BODY.rotation, Quaternion.LookRotation(relativeDirection), rotationTimeFaceToMouse));
            }
        }

        //if (this.transform.GetComponent<ManualInput>().enabled)
        //{
        //    //*** Rotate character by mouse position.
        //    Ray ray = Camera.main.ScreenPointToRay(MousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit, 100f))
        //    {
        //        Vector3 move = this.transform.InverseTransformPoint(hit.point);
        //        float m_TurnAmount = Mathf.Atan2(move.x, move.z);
        //        ForwardAmount = move.z;
        //        float turnSpeed = Mathf.Lerp(StationaryTurnSpeed, MovingTurnSpeed, ForwardAmount);
        //        deltaRotation = Quaternion.Euler(0, m_TurnAmount * turnSpeed * Time.fixedDeltaTime, 0);
        //    }
        //    else
        //    {
        //        deltaRotation = Quaternion.identity;
        //    }

        //    //Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 30, 0) * Time.deltaTime);
        //    RIGID_BODY.MoveRotation(RIGID_BODY.rotation * deltaRotation);
        //}
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

    IEnumerator SetIsMovindToFalse(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isMoving = false;
    }

    //private void OnAnimatorMove()
    //{
    //    RIGID_BODY.velocity = Velocity;
    //}

    //private void OnAnimatorMove()
    //{
    //    // we implement this function to override the default root motion.
    //        // this allows us to modify the positional speed before it's applied.
    //        if (m_IsGrounded && Time.deltaTime > 0)
    //        {
    //            Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

    //            // we preserve the existing y part of the current velocity.
    //            v.y = m_Rigidbody.velocity.y;
    //            m_Rigidbody.velocity = v;
    //        }
    //}
    //private void ClickToMove(Vector3 position)
    //{
    //    //Debug.Log(Vector3.Distance(transform.position, raycastHit.point));
    //    if (Vector3.Distance(transform.position, position) > StoppingDistance)
    //    {
    //        //VirtualInputManager.Instance.PressedW = true;
    //        //VirtualInputManager.Instance.KeyboardPressedValue.y = VirtualInputManager.Instance.KeyboardPressedValue.y;

    //        var targetInput = transform.InverseTransformPoint(position);
    //        dirClickToMove = Vector3.Lerp(dirClickToMove, targetInput, Time.deltaTime * speed);
    //        rigid.MovePosition(Vector3.MoveTowards(transform.position, position, Time.deltaTime * speed)); //Time.deltaTime * dirClickToMove.magnitude

    //        //Rotate to target
    //        float turnSpeed = Mathf.Lerp(StationaryTurnSpeed, MovingTurnSpeed, ForwardAmount);
    //        float turnAmount = Mathf.Atan2(targetInput.x, targetInput.z);
    //        Quaternion deltaRotation = Quaternion.Euler(0, turnAmount * turnSpeed * Time.deltaTime, 0);
    //        rigid.MoveRotation(rigid.rotation * deltaRotation);
    //    }
    //    else
    //    {
    //        VirtualInputManager.Instance.PressedW = false;
    //        MouseClicked = false;
    //    }
    //}
    //IEnumerator TestCo()
    //{
    //    //rigid.AddForce(Vector3.up * 50f);
    //    isMoving = true;
    //    var targetInput = transform.InverseTransformPoint(ClickPosition);
    //    while (Vector3.Distance(transform.position, targetInput) > StoppingDistance)
    //    {
    //        PressedW = true;

    //        //Rotate to target
    //        float turnSpeed = Mathf.Lerp(StationaryTurnSpeed, MovingTurnSpeed, ForwardAmount);
    //        //ForwardAmount += 0.2f * Time.deltaTime;
    //        Debug.Log(turnSpeed);
    //        float turnAmount = Mathf.Atan2(targetInput.x, targetInput.z);
    //        Quaternion delta = Quaternion.Euler(0, turnAmount * turnSpeed * Time.fixedDeltaTime, 0);
    //        rigid.MoveRotation(rigid.rotation * delta);



    //        //dirClickToMove = Vector3.Lerp(dirClickToMove, targetInput, Time.fixedDeltaTime * speed);
    //        //rigid.MovePosition(Vector3.MoveTowards(transform.position, ClickPosition, Time.fixedDeltaTime * speed)); //Time.deltaTime * dirClickToMove.magnitude


    //        //Quaternion q = Quaternion.AngleAxis(turnAmount * turnSpeed * Time.fixedDeltaTime, Vector3.up);
    //        //Quaternion q = Quaternion.LookRotation(targetInput, Vector3.up);
    //        //rigid.MoveRotation(rigid.transform.rotation * q);


    //        //var currentRotation = rigid.rotation;
    //        //var horizontalDirection = Vector3.ProjectOnPlane(targetInput, Vector3.up);
    //        //var targetRotation = Quaternion.LookRotation(horizontalDirection);
    //        //var newRotation = Quaternion.Slerp(
    //        //    currentRotation, // mix where the rig points now
    //        //    targetRotation,  // with where it should point
    //        //    speed * Time.fixedDeltaTime); // with this ratio
    //        //Debug.Log(newRotation);
    //        //rigid.MoveRotation(newRotation);


    //        yield return null;
    //    }
    //    isMoving = false;
    //    PressedW = false;
    //    MustMove = true;
    //}
    //private void TestCo1()
    //{
    //    //rigid.AddForce(Vector3.up * 50f);
    //    var targetInput = transform.InverseTransformPoint(ClickPosition);
    //    if (Vector3.Distance(rigid.position, ClickPosition) > StoppingDistance)
    //    {
    //        isMoving = true;
    //        PressedW = true;

    //        //Rotate to target
    //        float turnSpeed = Mathf.Lerp(StationaryTurnSpeed, MovingTurnSpeed, ForwardAmount);
    //        turnAmount = Mathf.Atan2(targetInput.x, targetInput.z);

    //        Quaternion deltax = Quaternion.Euler(0, turnAmount * turnSpeed * Time.deltaTime, 0);
    //        Quaternion deltaR = Quaternion.AngleAxis(turnAmount * turnSpeed * Time.deltaTime, Vector3.up);
    //        rigid.MoveRotation(deltaR * rigid.rotation);
    //        //Debug.Log("Rotate");
    //    }
    //    else
    //    {
    //        isMoving = false;
    //        PressedW = false;
    //        MustMove = true;
    //        return;
    //    }
    //}
}
