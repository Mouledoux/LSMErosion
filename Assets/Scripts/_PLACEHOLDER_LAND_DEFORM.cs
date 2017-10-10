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
                //Vector3 newVert = vertices[i] + transform.InverseTransformDirection(rb.velocity.normalized * rb.mass) * Mathf.Abs(dist - 1);
                //StartCoroutine(SmoothLandDeform(i, newVert));
            }
        }
        
        m_mesh.vertices = vertices;
        m_collider.sharedMesh = m_mesh;

        Destroy(other.gameObject);
    }

    public IEnumerator SmoothLandDeform(int vertIndex, Vector3 newVert)
    {
        Vector3[] vertices = m_mesh.vertices;

        while (Vector3.Distance(vertices[vertIndex], newVert) > 0.01f)
        {
            vertices[vertIndex] = Vector3.Lerp(vertices[vertIndex], newVert, Time.deltaTime);

            m_mesh.vertices = vertices;
            m_collider.sharedMesh = m_mesh;

            yield return null;
        }
    }
}

