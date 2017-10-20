using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGeneration : MonoBehaviour
{
    public GameObject[] m_generationObjectPrefabs;
    public TowerStorage m_towerStorage;

    public int m_generationIndex = 0;

    public float m_chargeingTime;


	// Use this for initialization
	void Start ()
    {
		foreach (GameObject go in m_generationObjectPrefabs)
        {
            if (go.GetComponent<TowerBase>() == null) Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        Transform freeSpot = null;
        foreach (Transform t in m_towerStorage.m_towerStorageSpots)
        {
            if (t.childCount == 0)
            {
                freeSpot = t;
                break;
            }
        }

        if (!freeSpot)
        {
            m_chargeingTime = 0;
            return;
        }

        m_chargeingTime += Time.deltaTime + Random.value * Time.deltaTime;

        if(m_chargeingTime >= m_generationObjectPrefabs[m_generationIndex].GetComponent<TowerBase>().m_cost)
        {
            m_chargeingTime = 0;

            GameObject g = Instantiate(m_generationObjectPrefabs[m_generationIndex], transform.position + (transform.up * 0.1f), Quaternion.identity);
            g.transform.parent = freeSpot;

            StartCoroutine(TowerToStand(g));
        }
	}

    public IEnumerator TowerToStand(GameObject tower)
    {
        float dist = float.MaxValue;
        Vector3 oPos = tower.transform.localPosition;

        while (dist > 0.01f)
        {
            tower.transform.localPosition = Vector3.Lerp(tower.transform.localPosition, Vector3.zero, Time.deltaTime);
            dist = tower.transform.localPosition.magnitude;

            yield return null;
        }

        tower.transform.localPosition = Vector3.zero;
    }

}
