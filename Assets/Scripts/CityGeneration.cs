using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGeneration : MonoBehaviour
{
    public GameObject[] m_generationObjectPrefabs;
    public TowerStorage m_towerStorage;
    public GameObject m_towerPreview;
    public Material m_previreMaterial;

    [SerializeField]
    private float m_chargeingTime;
    private int m_generationIndex = -1;

    private Transform m_towerStoragePos;

    public AudioSource m_ready;

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
        if (m_generationIndex < 0) return;

        foreach (Transform t in m_towerStorage.m_towerStorageSpots)
        {
            if (m_towerStoragePos != null) break;

            if (t.childCount == 0)
            {
                m_towerStoragePos = t;
                m_towerPreview.SetActive(true);
                m_towerPreview.transform.parent = m_towerStoragePos;
                m_towerPreview.transform.localPosition = Vector3.zero;
                break;
            }
        }


        if (!m_towerStoragePos)
        {
            m_chargeingTime = 0;
            m_towerPreview.SetActive(false);
            return;
        }


        m_chargeingTime += Time.deltaTime;

        if(m_chargeingTime >= m_generationObjectPrefabs[m_generationIndex].GetComponent<TowerBase>().m_cost)
        {
            m_chargeingTime = Random.value;

            GameObject g = Instantiate(m_generationObjectPrefabs[m_generationIndex], m_towerStoragePos.position, Quaternion.identity);
            g.transform.parent = m_towerStoragePos;
            m_towerStoragePos = null;
            m_ready.Play();
        }
	}

    public IEnumerator TowerToStand(GameObject tower)
    {
        float dist = float.MaxValue;
        Vector3 oPos = tower.transform.localPosition;
        float timer = 0;


        while (dist > 0.01f)
        {
            Vector3 newPos = Vector3.Lerp(oPos, Vector3.zero, timer);
            tower.transform.localPosition = newPos;
            dist = tower.transform.localPosition.magnitude;
            timer += Time.deltaTime;
            yield return null;
        }

        tower.transform.localPosition = Vector3.zero;
    }

    [ContextMenu("SetIndex")]
    void ABC()
    {
        ++m_generationIndex;
        SetGenIndex(m_generationIndex);
    }
    

    public void SetGenIndex(int i)
    {
        i = i >= m_generationObjectPrefabs.Length? 0 : i;

        m_generationIndex = i;
        SetPreview(m_generationObjectPrefabs[i].GetComponent<TowerBase>().m_preview);
    }


    public void SetPreview(GameObject preview)
    {
        Destroy(m_towerPreview);
        m_towerPreview = Instantiate(preview, m_towerStoragePos);
    }
}
