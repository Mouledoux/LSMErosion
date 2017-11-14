using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionColorToAudioBand : MonoBehaviour
{
    [SerializeField]
    private AudioVisualization m_AV;

    [SerializeField]
    private int m_AudioBand;

    [SerializeField]
    private Material m_Material;

    [SerializeField]
    private Color m_NewColor;
    private Color m_OriginalColor;

    private float m_Frequency;

	void Start ()
    {
        if (m_Material == null)
        {
            m_Material = new Material(GetComponent<Renderer>().material);
            GetComponent<Renderer>().material = m_Material;
        }
        m_OriginalColor = m_Material.GetColor("_EmissionColor");

        m_AudioBand = (m_AudioBand > m_AV.frequencyBands) ? (m_AV.frequencyBands - 1) : m_AudioBand;
	}
	

	void Update ()
    {
        m_Frequency = m_AV.m_CurrentFrequencyStereo[m_AudioBand];
        m_Material.SetColor("_EmissionColor", Color.Lerp(m_OriginalColor, m_NewColor, m_Frequency * 2f));
	}

    private void OnDisable()
    {
        m_Material.SetColor("_EmissionColor", m_OriginalColor);
    }
}
