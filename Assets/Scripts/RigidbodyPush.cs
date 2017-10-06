using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyPush : MonoBehaviour
{
    public float force = 0.0f;
    public Vector3 direction;


	void Start ()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.InverseTransformDirection(direction) * force);
	}
}
