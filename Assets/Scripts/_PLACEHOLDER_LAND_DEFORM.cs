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

    private Mouledoux.Components.Mediator.Subscriptions m_subscriptions = new Mouledoux.Components.Mediator.Subscriptions();
    private Mouledoux.Callback.Callback deform;

	void Awake ()
    {
        m_mesh = GetComponent<MeshFilter>().mesh;
        m_collider = GetComponent<MeshCollider>();

        m_collider.sharedMesh = m_mesh;
        m_newVertPos = m_mesh.vertices;
        m_vertBuffer = m_mesh.vertices;

        deform = DeformMesh;

        m_subscriptions.Subscribe(gameObject.GetInstanceID().ToString() + "->deform", deform);
    }


    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb == null) return;

        DeformMesh(other.transform.position, rb.velocity, 0.01f);
    }

    public void DeformMesh(Vector3 pos, Vector3 dir, float force)
    {
        dir = dir.normalized;
        dir *= force;

        Vector3[] vertices = m_mesh.vertices;
        float dist = 0f;

        RaycastHit rh;
        Physics.Raycast(pos, dir, out rh);
        Vector3 POC = (rh.point);

        for (int i = 0; i < vertices.Length; i++)
        {
            dist = Vector3.Distance(transform.TransformPoint(vertices[i]), POC);

            if (dist <= 0.1f)
            {
                vertices[i] += transform.InverseTransformDirection(dir) * Mathf.Abs(dist - 1);

                vertices[i].x = vertices[i].x < 0 ? 0 : vertices[i].x;
                vertices[i].y = vertices[i].y < 0 ? 0 : vertices[i].y;
                vertices[i].z = vertices[i].z < 0 ? 0 : vertices[i].z;

                if (!m_affectedVerts.Contains(i)) m_affectedVerts.Add(i);
            }
        }

        m_mesh.vertices = vertices;
        m_collider.sharedMesh = m_mesh;
    }

    public void DeformMesh(Mouledoux.Callback.Packet packet)
    {
        Vector3 pos = new Vector3(packet.floats[0], packet.floats[1], packet.floats[2]);
        Vector3 dir = new Vector3(packet.floats[3], packet.floats[4], packet.floats[5]);
        float str = packet.floats[0];

        DeformMesh(pos, dir, str);
    }
}