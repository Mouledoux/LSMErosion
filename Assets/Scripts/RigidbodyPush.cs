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
        StartCoroutine(GrowShrink(Vector3.zero, transform.localScale));
	}

    public void Push()
    {
        Start();
    }

    public IEnumerator GrowShrink(Vector3 oScale, Vector3 nScale)
    {
        transform.localScale = oScale;

        while(Vector3.Distance(transform.localScale, nScale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, nScale, Time.deltaTime * 10);
            yield return null;
        }

        if (nScale == Vector3.zero)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Valve.VR.InteractionSystem.TeleportArea>()) return;

        StopAllCoroutines();
        StartCoroutine(GrowShrink(transform.localScale, Vector3.zero));
    }
}