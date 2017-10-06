using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class _PLACEHOLDER_LAND_DEFORM : MonoBehaviour
{
    private Mesh m_mesh;
    private MeshCollider m_collider;
    
	void Start ()
    {
        m_mesh = GetComponent<MeshFilter>().mesh;
        m_collider = GetComponent<MeshCollider>();

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb == null) return;

        Vector3[] vertices = m_mesh.vertices;
        
        float dist = 0f;

        Vector3 POC = collision.ClosestPoint(transform.position);

        for (int i = 0; i < vertices.Length; i++)
        {
            dist = Vector3.Distance(transform.position + vertices[i], POC);

            if (dist <= 0.5f)
            {
                vertices[i] /= rb.velocity.magnitude * 2;
            }
        }

        m_mesh.vertices = vertices;
        m_collider.sharedMesh = m_mesh;

        Destroy(collision.gameObject);
    }
}

