using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGeneration : MonoBehaviour
{
    public GameObject[] m_generationObjectPrefabs;
    public TowerStorage m_towerStorage;
    public GameObject m_towerPreviewCity;
    public GameObject m_towerPreviewBase;
    public Material m_previreMaterial;
    public Transform m_progressBar;


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
            else SetPreview(m_generationObjectPrefabs[m_generationIndex].GetComponent<TowerBase>().m_preview);

            if (t.childCount == 0)
            {
                m_towerStoragePos = t;
                m_towerPreviewBase.SetActive(true);
                m_towerPreviewBase.transform.parent = m_towerStoragePos;
                m_towerPreviewBase.transform.localPosition = Vector3.zero;
            }
        }


        if (!m_towerStoragePos)
        {
            m_chargeingTime = 0;
            m_towerPreviewBase.SetActive(false);
            return;
        }

        m_chargeingTime += Time.deltaTime;
        m_progressBar.localScale = new Vector3(m_chargeingTime / m_generationObjectPrefabs[m_generationIndex].GetComponent<TowerBase>().m_cost, 1, 1);

        if (m_chargeingTime >= m_generationObjectPrefabs[m_generationIndex].GetComponent<TowerBase>().m_cost)
        {
            m_chargeingTime = Random.value;

            GameObject g = Instantiate(m_generationObjectPrefabs[m_generationIndex], m_towerStoragePos.position, Quaternion.identity);
            g.transform.parent = m_towerStoragePos;
            m_towerStoragePos = null;
            m_ready.Play();

            m_progressBar.localScale = Vector3.one;
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
    

    public void SetGenIndex(int i)
    {
        i = i >= m_generationObjectPrefabs.Length? 0 : i;

        m_generationIndex = i;
        SetPreview(m_generationObjectPrefabs[i].GetComponent<TowerBase>().m_preview);
    }


    public void SetPreview(GameObject preview)
    {
        Destroy(m_towerPreviewBase);
        Destroy(m_towerPreviewCity);
        m_towerPreviewBase = Instantiate(preview, m_towerStoragePos);
        m_towerPreviewCity = Instantiate(preview, transform);
        m_towerPreviewCity.transform.localPosition = new Vector3(0, 0, 0.125f);
        m_towerPreviewCity.transform.Rotate(90, 0, 0);
    }
}
