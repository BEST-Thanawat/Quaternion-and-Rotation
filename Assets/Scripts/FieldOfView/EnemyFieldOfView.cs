using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public float RotationSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddForce()
    {
        Vector3 force = new Vector3(0, 100f, 0);
        transform.GetComponent<Rigidbody>().AddForce(force);
    }

    public void RotateToPlayer()
    {
        StartCoroutine(Rotate());
    }
    IEnumerator Rotate()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        Debug.Log(1);
        while (transform.rotation != lookRotation)
        {
            direction = (player.transform.position - transform.position).normalized;
            lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            //Debug.Log(lookRotation);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
            //Debug.Log(transform.rotation);
            yield return null;

            //turnAmount = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, player.transform.rotation, RotationSpeed * Time.deltaTime);
            //turnAmount = Mathf.Atan2(direction.normalized.x, direction.normalized.z);

            //Quaternion lookRotation = Quaternion.LookRotation(direction);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, RotationSpeed * Time.deltaTime);
            //Debug.Log(Math.Abs(turnAmount));
            //yield return null;

            ////find the vector pointing from our position to the target
            //_direction = (Target.position - transform.position).normalized;

            ////create the rotation we need to be in to look at the target
            //_lookRotation = Quaternion.LookRotation(_direction);

            ////rotate us over time according to speed until we are in the required rotation
            //transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);

        }
    }
}
