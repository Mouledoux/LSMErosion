using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDredgeIsland : MonoBehaviour
{
    private Vector3 m_targetScale;
    private Vector3 m_maxScale;

    private void Start()
    {
        m_maxScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, m_targetScale, Time.deltaTime);
        m_targetScale += m_targetScale.magnitude >= m_maxScale.magnitude ? Vector3.zero : (m_maxScale * Time.deltaTime) / 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        m_targetScale *= 0.1f;
       if (m_targetScale.magnitude < m_maxScale.magnitude * 0.05f) Destroy(gameObject);
    }
}
