using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PLACEHOLDER_ENEMY_SPAWN : MonoBehaviour
{
    public float m_spawnDelay;
    private float m_timer;

    public GameObject m_enemy;
    public GameObject m_speciamEnemy;

	void Update ()
    {
        m_timer += Time.deltaTime;

        if(m_timer >= m_spawnDelay)
        {
            Instantiate(m_enemy, transform.position, transform.rotation, transform.parent);
            m_timer = Random.value;

            if(m_timer < 0.1) Instantiate(m_speciamEnemy, transform.position, transform.rotation, transform.parent);
        }
	}
}
