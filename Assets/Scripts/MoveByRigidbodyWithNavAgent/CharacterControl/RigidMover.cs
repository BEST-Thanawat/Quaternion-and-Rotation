using System;
using UnityEngine;

public class RigidMover : MonoBehaviour
{
	[SerializeField, Range(0f, 100f)]
	float maxSpeed = 0f;

	[SerializeField, Range(0f, 100f)]
	float maxAcceleration = 100f, maxAirAcceleration = 0f;

	[SerializeField, Range(0f, 10f)]
	float jumpHeight = 0f;

	[SerializeField, Range(0, 5)]
	int maxAirJumps = 0;

	[SerializeField, Range(0, 90)]
	float maxGroundAngle = 89f;
	[SerializeField, Range(0, 90)]
	float maxStairsAngle = 89f;

	[SerializeField, Range(0f, 100f)]
	float maxSnapSpeed = 100f;

	[SerializeField, Min(0f)]
	float probeDistance = 1f;

	[SerializeField]
	LayerMask probeMask = -1, stairsMask = -1;

	//Rigidbody rigid;
	Vector3 velocity, desiredVelocity;
	//bool desiredJump;
	Vector3 contactNormal, steepNormal;
	int groundContactCount, steepContactCount;

	[SerializeField]
	private bool OnGround;
	[SerializeField]
	private bool IsDebugging;
	//bool OnGround => groundContactCount > 0;
	bool OnSteep => steepContactCount > 0;
	//int jumpPhase;
	float minGroundDotProduct, minStairsDotProduct;
	int stepsSinceLastGrounded, stepsSinceLastJump;
	private CharacterControl characterControl;

	void OnValidate()
	{
		minGroundDotProduct = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad); //Ex. Angle 90 degree convert to radius to find Cos
		minStairsDotProduct = Mathf.Cos(maxStairsAngle * Mathf.Deg2Rad);
	}

	void Awake()
	{
		characterControl = GetComponent<CharacterControl>();
		//rigid = characterControl.RIGID_BODY; //GetComponent<Rigidbody>();
		OnValidate();
	}

	void Update()
	{
		OnGround = groundContactCount > 0 ? true : false;

		//Vector2 playerInput;
		//playerInput.x = characterControl.KeyboardPressedValue.x ;
		//playerInput.y = characterControl.KeyboardPressedValue.y;
		//playerInput = Vector2.ClampMagnitude(playerInput, 1f);

		desiredVelocity = characterControl.RIGID_BODY.rotation * new Vector3(characterControl.KeyboardPressedValue.x, 0f, characterControl.KeyboardPressedValue.y) * maxSpeed;
		//desiredJump |= VirtualInputManager.Instance.Jump;
	}

	void FixedUpdate()
	{
		UpdateState();
		AdjustVelocity();
		//if (desiredJump)
		//{
		//	desiredJump = false;
		//	Jump();
		//}
		//Vector3 force = new Vector3(playerInput.x, 0.0F, zForce);

		//characterControl.RIGID_BODY.velocity = Vector3.Project(velocity, characterControl.ClickPosition.normalized);
		//characterControl.RIGID_BODY.AddRelativeForce(characterControl.ClickPosition);

		characterControl.RIGID_BODY.velocity = velocity;  //We also set velocity to CharacterControl to help root motion movement.
		ClearState();
	}

	void ClearState()
	{
		groundContactCount = steepContactCount = 0;
		contactNormal = steepNormal = Vector3.zero;
	}

	void UpdateState()
	{
		stepsSinceLastGrounded += 1;
		//stepsSinceLastJump += 1;
		velocity = characterControl.RIGID_BODY.velocity;
		if (OnGround || SnapToGround() || CheckSteepContacts())
		{
			stepsSinceLastGrounded = 0;
			//if (stepsSinceLastJump > 1)
			//{
			//	jumpPhase = 0;
			//}
			if (groundContactCount > 1)
			{
				contactNormal.Normalize();
			}
		}
		else
		{
			contactNormal = Vector3.up;
		}
	}

	bool SnapToGround()
	{
		if (stepsSinceLastGrounded > 1 || stepsSinceLastJump <= 2)
		{
			return false;
		}
		float speed = velocity.magnitude;
		if (speed > maxSnapSpeed)
		{
			return false;
		}

		RaycastHit hit;

		if(!Utility.Instance.IsGrounded(characterControl, probeDistance, out hit, probeMask))
		{
			return false;
		}
		//if (!Physics.Raycast(characterControl.RIGID_BODY.position, Vector3.down, out RaycastHit hit, probeDistance, probeMask))
		//{
		//	return false;
		//}

		if (hit.normal.y < GetMinDot(hit.collider.gameObject.layer))
		{
			return false;
		}

		groundContactCount = 1;
		contactNormal = hit.normal;
		float dot = Vector3.Dot(velocity, hit.normal);
		if (dot > 0f)
		{
			velocity = (velocity - hit.normal * dot).normalized * speed;
		}

		return true;
	}

	bool CheckSteepContacts()
	{
		if (steepContactCount > 1)
		{
			steepNormal.Normalize();
			if (steepNormal.y >= minGroundDotProduct) //float.Parse(minGroundDotProduct.ToString("F2")))
			{
				steepContactCount = 0;
				groundContactCount = 1;
				contactNormal = steepNormal;
				return true;
			}
		}
		return false;
	}

	void AdjustVelocity()
	{
		Vector3 xAxis = ProjectOnContactPlane(Vector3.right).normalized;
		Vector3 zAxis = ProjectOnContactPlane(Vector3.forward).normalized;

		float currentX = Vector3.Dot(velocity, xAxis);
		float currentZ = Vector3.Dot(velocity, zAxis);

		float acceleration = OnGround ? maxAcceleration : maxAirAcceleration;
		float maxSpeedChange = acceleration * Time.deltaTime;

		float newX = Mathf.MoveTowards(currentX, desiredVelocity.x, maxSpeedChange);
		float newZ = Mathf.MoveTowards(currentZ, desiredVelocity.z, maxSpeedChange);
		
		velocity += xAxis * (newX - currentX) + zAxis * (newZ - currentZ);
	}

	//void Jump()
	//{
	//	Vector3 jumpDirection;
	//	if (OnGround)
	//	{
	//		jumpDirection = contactNormal;
	//	}
	//	else if (OnSteep)
	//	{
	//		jumpDirection = steepNormal;
	//		jumpPhase = 0;
	//	}
	//	else if (maxAirJumps > 0 && jumpPhase <= maxAirJumps)
	//	{
	//		if (jumpPhase == 0)
	//		{
	//			jumpPhase = 1;
	//		}
	//		jumpDirection = contactNormal;
	//	}
	//	else
	//	{
	//		return;
	//	}

	//	stepsSinceLastJump = 0;
	//	jumpPhase += 1;
	//	float jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
	//	jumpDirection = (jumpDirection + Vector3.up).normalized;
	//	float alignedSpeed = Vector3.Dot(velocity, jumpDirection);
	//	if (alignedSpeed > 0f)
	//	{
	//		jumpSpeed = Mathf.Max(jumpSpeed - alignedSpeed, 0f);
	//	}
	//	velocity += jumpDirection * jumpSpeed;
	//}

	void OnCollisionEnter(Collision collision)
	{
		EvaluateCollision(collision);
	}

	void OnCollisionStay(Collision collision)
	{
		EvaluateCollision(collision);
	}

	void EvaluateCollision(Collision collision)
	{
		float minDot = GetMinDot(collision.gameObject.layer);
		for (int i = 0; i < collision.contactCount; i++)
		{
			Vector3 normal = collision.GetContact(i).normal;
			
			if (normal.y >= minDot)
			{
				groundContactCount += 1;
				contactNormal += normal;
			}
			else if (normal.y > -0.01f)
			{
				steepContactCount += 1;
				steepNormal += normal;
			}
			if (IsDebugging)
			{
				Debug.DrawRay(characterControl.RIGID_BODY.position, contactNormal.normalized, Color.blue, 0.5f);
				Debug.DrawRay(characterControl.RIGID_BODY.position, normal.normalized, Color.cyan, 0.5f);
			}
		}
	}

	Vector3 ProjectOnContactPlane(Vector3 vector)
	{
		//if (IsDebugging) Debug.DrawRay(characterControl.RIGID_BODY.position, vector - contactNormal * Vector3.Dot(vector, contactNormal), Color.red, 0.5f);
		return vector - contactNormal * Vector3.Dot(vector, contactNormal);
	}

	float GetMinDot(int layer)
	{
		return (stairsMask & (1 << layer)) == 0 ?
			minGroundDotProduct : minStairsDotProduct;
	}
}