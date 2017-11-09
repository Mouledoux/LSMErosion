using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyPush : MonoBehaviour
{
    public float force = 0.0f;
    public Vector3 direction;
    public float m_growthTime;
    public bool m_pushOnAwake;
    Rigidbody rb;

    public int health;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(GrowShrink(Vector3.zero, transform.localScale));

        if (m_pushOnAwake) Push();
	}

    public void Push()
    {
        rb.velocity = transform.TransformDirection(direction.normalized) * force;
    }

    public IEnumerator GrowShrink(Vector3 oScale, Vector3 nScale)
    {
        transform.localScale = oScale;

        while(Vector3.Distance(transform.localScale, nScale) > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, nScale, Time.deltaTime * (1f / m_growthTime));
            yield return null;
        }

        if (nScale == Vector3.zero)
        {
            GetComponent<Collider>().enabled = false;

            yield return new WaitWhile(() => GetComponent<AudioSource>().isPlaying);

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Valve.VR.InteractionSystem.TeleportArea>() ||
            other.gameObject.layer == LayerMask.NameToLayer("Water") ||
            other.GetComponent<Rigidbody>() != null ||
            (!other.CompareTag(tag) && other.gameObject.layer != LayerMask.NameToLayer("Land"))) return;

        AudioSource audio = GetComponent<AudioSource>();
        audio.loop = false;
        audio.Play();

        health--;

        if (health <= 0 || other.gameObject.layer == LayerMask.NameToLayer("Land"))
        {
            StopAllCoroutines();
            GetComponent<Collider>().enabled = false;
            StartCoroutine(GrowShrink(transform.localScale, Vector3.zero));
        }


    }
}