using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : InteractableObject
{
    public string m_towerType;
    public float m_cost;

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
        base.OffInteract(packet);
    }
}
