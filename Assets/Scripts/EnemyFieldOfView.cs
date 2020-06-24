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
        while (Mathf.Atan2(transform.position.x, transform.position.z) > 0.1f)
        {
            Vector3 direction = player.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, RotationSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
