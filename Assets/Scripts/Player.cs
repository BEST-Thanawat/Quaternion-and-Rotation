using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Space(3)]
    [Header("Quaternion", order = 0)]
    [SerializeField] private GameObject enemy;
    [SerializeField] private Text distanceToEnemy;
    public float RotationSpeed = 4f;

    
    float smoothInputMagnitude;
    float smoothMoveVelocity;
    float angle;
    Vector3 velocity;
    Rigidbody rigidbody;

    [Space(3)]
    [Header("Movement", order = 1)]
    public float moveSpeed = 7;
    public float turnSpeed = 8f;
    public float smoothMoveTime = 0.1f;
    

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rigidbody.MoveRotation(Quaternion.Euler(Vector3.up * angle));
        rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime);
    }

    private void Update()
    {
        Vector3 inputDirection = Vector3.zero;
        inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        float inputMagnitude = inputDirection.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothMoveVelocity, smoothMoveTime);

        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        angle = Mathf.LerpAngle(angle, targetAngle, Time.deltaTime * turnSpeed * inputMagnitude);

        velocity = transform.forward * moveSpeed * smoothInputMagnitude;
    }

    public void Rotate90ToEnemy()
    {
        StartCoroutine(Rotate90CT());
    }

    public void DrawDistance()
    {
        StartCoroutine(DrawDistanceCT());
    }
    public void DrawDirection()
    {
        StartCoroutine(DrawDirectionCT());
    }
    IEnumerator Rotate90CT()
    {
        while (true)
        {
            Vector3 direction = (enemy.transform.position - transform.position).normalized;
            float distance = (enemy.transform.position - transform.position).magnitude;

            Vector3 relativePosition = (enemy.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(relativePosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, RotationSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, RotationSpeed * Time.deltaTime);
            //float targetAngle = Mathf.Atan2(relativePosition.z, relativePosition.x) * Mathf.Rad2Deg;
            //Quaternion offset = Quaternion.Euler(0, 90, 0);
            //Quaternion targetAngle = Quaternion.Euler(relativePosition.z, 0, relativePosition.x);
            //transform.rotation = Quaternion.Lerp(transform.rotation, targetAngle, RotationSpeed * Time.deltaTime);

            
            yield return null;
        }
    }

    IEnumerator DrawDistanceCT()
    {
        while (true)
        {
            Vector3 direction = (enemy.transform.position - transform.position);
            float distance = direction.magnitude;

            Vector3 directionLine = transform.position + (direction.normalized * distance);
            Debug.DrawLine(transform.position, directionLine, Color.red);
            distanceToEnemy.text = "Distance between player and enemy (Red Line): " + distance.ToString();

            yield return null;
        }
    }
    IEnumerator DrawDirectionCT()
    {
        while (true)
        {
            Vector3 direction = (enemy.transform.position - transform.position);
            Vector3 directionLine = transform.position + (direction.normalized * 2f);
            Debug.DrawLine(transform.position, directionLine, Color.blue);
            yield return null;
        }
    }
}
