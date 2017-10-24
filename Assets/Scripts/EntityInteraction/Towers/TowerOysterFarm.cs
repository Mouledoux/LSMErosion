using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerOysterFarm : MonoBehaviour
{
    public int m_health;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb == null) return;

        other.enabled = false;
        m_health--;


        if (m_health <= 0) Destroy(gameObject);
    }
}
