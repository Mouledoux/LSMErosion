using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerOysterFarmWindBreak : MonoBehaviour
{
    public int m_health;
    
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();

        if (rb == null || !other.CompareTag(tag)) return;

        other.enabled = false;
        m_health--;

        if (m_health <= 0) StartCoroutine(iDestroy());
    }

    public IEnumerator iDestroy()
    {
        Vector3 nPos = transform.localPosition;
        nPos.y *= -1;

        float timer = 1;

        while(timer > 0)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, nPos, Time.deltaTime);
            timer -= Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }
}
