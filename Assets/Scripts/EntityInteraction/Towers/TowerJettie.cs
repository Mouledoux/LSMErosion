using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerJettie : MonoBehaviour
{
    public float m_growthDelay;

    [SerializeField]
    private float m_offset;

    private RaycastHit m_raycast;


    private void Start()
    {
        StartCoroutine(GrowLand());
        //StartCoroutine(SnapToLand());
    }

    public IEnumerator iSnapToLand()
    {
        Vector3 rayPos = transform.position;
        rayPos.y *= 1.001f;
        Physics.Raycast(rayPos, -transform.forward, out m_raycast);

        float timer = 0;
        Vector3 forwardOffset = transform.forward * 0.05f;

        while (Vector3.Distance(transform.position, m_raycast.point + forwardOffset) > 0.01f)
        {
            transform.position = Vector3.Lerp(rayPos, m_raycast.point + forwardOffset, timer);

            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = m_raycast.point + forwardOffset;
    }

    [ContextMenu("Snap")]
    public void SnapToLand()
    {
        StartCoroutine(iSnapToLand());
    }

    public IEnumerator GrowLand()
    {
        Mouledoux.Callback.Packet packet = new Mouledoux.Callback.Packet();
        while (enabled)
        {
            Vector3 rayPos = transform.position + (transform.right * m_offset);
            rayPos.y *= 1.01f;
            Physics.Raycast(rayPos, -transform.forward, out m_raycast);
            Vector3 rayDir = m_raycast.point - rayPos;

            packet.floats = new float[] { rayPos.x, rayPos.y, rayPos.z, rayDir.x, rayDir.y, rayDir.z, -0.01f };
            Mouledoux.Components.Mediator.instance.NotifySubscribers(m_raycast.transform.gameObject.GetInstanceID().ToString() + "->deform", packet);

            yield return new WaitForSeconds(m_growthDelay);
        }
    }
}
