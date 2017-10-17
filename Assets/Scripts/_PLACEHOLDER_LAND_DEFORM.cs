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

        float max = float.MaxValue;

        for (int i = 0; i < vertices.Length; i++)
        {
            dist = Vector3.Distance(transform.TransformPoint(vertices[i]), POC);

            if (dist <= 0.1)
            {
                vertices[i] += transform.InverseTransformDirection(rb.velocity.normalized * rb.mass) * Mathf.Abs(dist - 1);
                vertices[i] = vertices[i].magnitude < m_mesh.vertices[i].magnitude ? vertices[i] : m_mesh.vertices[i];
            }
        }

        m_mesh.vertices = vertices;
        m_collider.sharedMesh = m_mesh;

        //other.enabled = false;
        //Destroy(other.gameObject);

        StartCoroutine(FadeOut(other.gameObject));
    }

    IEnumerator FadeOut(GameObject go)
    {
        while(go.transform.localScale.magnitude > 0)
        {
            go.transform.localScale = Vector3.Lerp(go.transform.localScale, Vector3.zero, Time.deltaTime * 4);
            yield return null;
        }
    }
}

