using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class _PLACEHOLDER_LAND_DEFORM : MonoBehaviour
{
    [Range(0.01f, 1.00f)]
    public float m_maxLoss;

    private Mesh m_mesh;
    private MeshCollider m_collider;

    private Vector3[] m_originalVerts;

    private Mouledoux.Components.Mediator.Subscriptions m_subscriptions = new Mouledoux.Components.Mediator.Subscriptions();
    private Mouledoux.Callback.Callback deform;

    private void Start()
    {
        m_mesh = GetComponent<MeshFilter>().mesh;
        m_collider = GetComponent<MeshCollider>();

        m_collider.sharedMesh = m_mesh;
        m_originalVerts = m_mesh.vertices;

        deform = DeformMesh;

        m_subscriptions.Subscribe(gameObject.GetInstanceID().ToString() + "->deform", deform);
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb == null) return;

        DeformMesh(other.transform.position, rb.velocity, 0.01f);
    }

    private void OnDestroy()
    {
        m_subscriptions.UnsubscribeAll();
        print(CalculateLandRemaining());
    }

    public void DeformMesh(Vector3 pos, Vector3 dir, float force)
    {
        dir = dir.normalized;

        Vector3[] vertices = m_mesh.vertices;
        float dist = 0f;

        RaycastHit rh;
        Physics.Raycast(pos, dir, out rh);
        Vector3 POC = (rh.point);
        
        for (int i = 0; i < vertices.Length; i++)
        {
            dist = Vector3.Distance((vertices[i]), transform.InverseTransformPoint(POC));

            if (dist <= 0.05f)
            {
                //if ((m_originalVerts[i].magnitude - vertices[i].magnitude) >= (1f - m_maxLoss))
                //{
                //    continue;
                //}

                vertices[i] += transform.InverseTransformDirection(dir) * force * Mathf.Abs(dist - 1);
            }
        }

        m_mesh.vertices = vertices;
        m_collider.sharedMesh = m_mesh;
    }

    public void DeformMesh(Mouledoux.Callback.Packet packet)
    {
        Vector3 pos = new Vector3(packet.floats[0], packet.floats[1], packet.floats[2]);
        Vector3 dir = new Vector3(packet.floats[3], packet.floats[4], packet.floats[5]);
        float str = packet.floats[6];

        DeformMesh(pos, dir, str);
    }


    public float CalculateLandRemaining()
    {
        float oMag = 0;
        float nMag = 0;

        foreach (Vector3 v in m_originalVerts)
        {
            oMag += v.magnitude;
        }

        for (int i = 0; i < m_originalVerts.Length; ++i)
        {
            nMag += Vector3.Distance(m_originalVerts[i], m_mesh.vertices[i]);
        }

        return (((oMag - nMag) / oMag));

        //foreach(vector3 v in m_originalverts)
        //{
        //    oMag += v.magnitude;
        //}

        //foreach(Vector3 v in m_mesh.vertices)
        //{
        //    nMag += v.magnitude;
        //}
        
        //return ((nMag / oMag) - (m_maxLoss)) / (1f - m_maxLoss);
    }
}