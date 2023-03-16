using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addForceOfTrigger : MonoBehaviour
{
    public Vector3 turn;
    public GameObject center;
    public float force = 4;
    public Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.AddForce((center.transform.position - transform.position) * force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
       
    }

}
