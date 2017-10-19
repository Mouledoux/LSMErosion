using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : InteractableObject
{
    // ---------- ---------- ---------- ---------- ----------
    private void Start()
    {
        Initialize(gameObject);
    }

    override protected void OnHighlight(Mouledoux.Callback.Packet packet)
    {
        base.OnHighlight(packet);
    }

    override protected void OffHighlight(Mouledoux.Callback.Packet packet)
    {
        base.OffHighlight(packet);
    }

    override protected void OnInteract(Mouledoux.Callback.Packet packet)
    {
        base.OnInteract(packet);
    }

    override protected void OffInteract(Mouledoux.Callback.Packet packet)
    {
        m_pickup = m_repickup;
        base.OffInteract(packet);
    }
}
