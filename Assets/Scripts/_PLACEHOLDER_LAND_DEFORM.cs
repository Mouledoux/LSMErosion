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

        m_collider.sharedMesh = m_mesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb == null) return;

        Vector3[] vertices = m_mesh.vertices;

        float dist = 0f;
        
        Vector3 POC = other.ClosestPoint(transform.position);

        for (int i = 0; i < vertices.Length; i++)
        {
            dist = Vector3.Distance(transform.TransformPoint(vertices[i]), POC);

            if (dist <= 1)
            {
                vertices[i] += transform.InverseTransformDirection(rb.velocity.normalized * rb.mass) * Mathf.Abs(dist - 1);
                vertices[i] = vertices[i].magnitude < m_mesh.vertices[i].magnitude ? vertices[i] : m_mesh.vertices[i];
            }
        }
        
        m_mesh.vertices = vertices;
        m_collider.sharedMesh = m_mesh;

        Destroy(other.gameObject);
    }
}

