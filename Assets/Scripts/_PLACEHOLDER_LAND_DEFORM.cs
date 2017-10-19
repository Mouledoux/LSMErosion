using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class _PLACEHOLDER_LAND_DEFORM : MonoBehaviour
{
    private Mesh m_mesh;
    private MeshCollider m_collider;

    private Vector3[] m_newVertPos;
    private Vector3[] m_vertBuffer;

    private List<int> m_affectedVerts = new List<int>();
    

	void Awake ()
    {
        m_mesh = GetComponent<MeshFilter>().mesh;
        m_collider = GetComponent<MeshCollider>();

        m_collider.sharedMesh = m_mesh;
        m_newVertPos = m_mesh.vertices;
        m_vertBuffer = m_mesh.vertices;
        
    }


    private void Update()
    {
        if (m_affectedVerts.Count < 1) return;

        for (int i = m_affectedVerts[0]; i < m_affectedVerts.Count; i++)
        {
            m_vertBuffer[m_affectedVerts[i]] = Vector3.Lerp(m_vertBuffer[m_affectedVerts[i]], m_newVertPos[m_affectedVerts[i]], 0.1f);

            if(Vector3.Distance(m_vertBuffer[m_affectedVerts[i]], m_newVertPos[m_affectedVerts[i]]) < 0.01f)
            {
                m_vertBuffer[m_affectedVerts[i]] = m_newVertPos[m_affectedVerts[i]];
                m_affectedVerts.Remove(m_affectedVerts[i]);
            }
            m_mesh.vertices = m_vertBuffer;
            m_collider.sharedMesh = m_mesh;
        }
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

            if (dist <= 0.1f)
            {
                vertices[i] += transform.InverseTransformDirection(rb.velocity.normalized * rb.mass) * Mathf.Abs(dist - 1);
                vertices[i] = vertices[i].magnitude < m_mesh.vertices[i].magnitude ? vertices[i] : m_mesh.vertices[i];

                if (!m_affectedVerts.Contains(i)) m_affectedVerts.Add(i);
            }
        }

        m_newVertPos = vertices;

        //m_mesh.vertices = vertices;
        //m_collider.sharedMesh = m_mesh;

        other.enabled = false;

        StartCoroutine(FadeOut(other.gameObject));
    }

    IEnumerator FadeOut(GameObject go)
    {
        while(go.transform.localScale.magnitude > 0)
        {
            go.transform.localScale = Vector3.Lerp(go.transform.localScale, Vector3.zero, Time.deltaTime * 4);
            yield return null;
        }

        Destroy(go);
    }
}

