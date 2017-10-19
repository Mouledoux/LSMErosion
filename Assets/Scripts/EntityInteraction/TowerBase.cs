using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : InteractableObject
{
    [SerializeField]
    private int m_health;
    public  int m_Health
    {
        get { return m_health; }

        set
        {
            m_health = value;

            if (m_health <= 0) Die();
        }
    }


    // ---------- ---------- ---------- ---------- ----------
    new private void Start()
    {
        base.Start();
    }

    new protected void OnHighlight(Mouledoux.Callback.Packet packet)
    {
        base.OnHighlight(packet);
    }

    new protected void OffHighlight(Mouledoux.Callback.Packet packet)
    {
        base.OffHighlight(packet);
    }

    new protected void OnInteract(Mouledoux.Callback.Packet packet)
    {
        base.OnInteract(packet);
    }

    new protected void OffInteract(Mouledoux.Callback.Packet packet)
    {
        m_pickup = m_repickup;
        base.OffInteract(packet);
    }

    protected void Die()
    {
        Destroy(gameObject);
    }
}
