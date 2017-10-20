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
        rb.velocity = transform.TransformDirection(direction.normalized) * force;
        StartCoroutine(GrowIn());
	}

    public void Push()
    {
        Start();
    }

    public IEnumerator GrowIn()
    {
        Vector3 oScale = transform.localScale;
        transform.localScale = Vector3.zero;

        while(Vector3.Distance(transform.localScale, oScale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, oScale, Time.deltaTime * 10);
            yield return null;
        }
    }
}
