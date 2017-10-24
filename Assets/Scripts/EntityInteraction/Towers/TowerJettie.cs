using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerJettie : MonoBehaviour
{
    public float m_growthRate;

    [SerializeField]
    private float m_offset;

    private RaycastHit m_raycast;


    private void Start()
    {
        StartCoroutine(GrowLand());
    }


    public IEnumerator SnapToLand()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(1 - m_growthRate);
        }
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

            yield return new WaitForSeconds(1 - m_growthRate);
        }
    }
}
