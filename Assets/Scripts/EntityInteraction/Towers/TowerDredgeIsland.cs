using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDredgeIsland : MonoBehaviour
{
    private Vector3 m_targetScale;
    private Vector3 m_maxScale = Vector3.one * 2;

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, m_targetScale, Time.deltaTime);
        m_targetScale += m_targetScale.magnitude >= m_maxScale.magnitude ? Vector3.zero : (Vector3.one * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        m_targetScale -= Vector3.one;
    }
}
