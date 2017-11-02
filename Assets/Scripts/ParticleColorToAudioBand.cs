using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleColorToAudioBand : MonoBehaviour
{
    [SerializeField]
    private AudioVisualization m_AV;

    [SerializeField]
    private int m_AudioBand;

    [SerializeField]
    private Color m_NewColor;
    private Color m_OriginalColor;

    private ParticleSystem.MainModule m_System;

    private float m_Frequency;

    void Start ()
    {
        m_System = GetComponent<ParticleSystem>().main;
        m_OriginalColor = m_System.startColor.color;
	}
	
	
	void Update ()
    {
        m_Frequency = m_AV.m_CurrentFrequencyStereo[m_AudioBand];
        m_System.startColor =
            new ParticleSystem.MinMaxGradient(Color.Lerp(m_OriginalColor, m_NewColor, m_Frequency));

    }
}
