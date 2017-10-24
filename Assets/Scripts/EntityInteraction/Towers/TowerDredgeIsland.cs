using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDredgeIsland : MonoBehaviour
{
    private Vector3 m_targetScale;
    private Vector3 m_maxScale;

    [SerializeField]
    private GameObject m_island;

    private void Start()
    {
        m_maxScale = m_island.transform.localScale;
        m_island.transform.localScale = m_maxScale * 0.01f;
    }

    private void Update()
    {
        m_island.transform.localScale = Vector3.Lerp(m_island.transform.localScale, m_targetScale, Time.deltaTime);
        m_targetScale += m_targetScale.magnitude >= m_maxScale.magnitude ? Vector3.zero : (m_maxScale * Time.deltaTime) / 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        m_targetScale *= 0.1f;
       if (m_targetScale.magnitude < m_maxScale.magnitude * 0.05f) Destroy(gameObject);
    }
}
