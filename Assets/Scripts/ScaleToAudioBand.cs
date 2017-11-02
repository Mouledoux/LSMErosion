using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToAudioBand : MonoBehaviour
{
    [SerializeField]
    private AudioVisualization m_AV;

    [SerializeField]
    private int m_AudioBand;

    [SerializeField]
    private float m_MaxScale;

    [SerializeField]
    private bool m_ScaleX, m_ScaleY, m_ScaleZ, m_LeftChannel, m_RightChannel;

    private Vector3 m_OriginalScale;
    private Vector3 m_NewScale;

    private float m_Frequency;

    private void Start()
    {
        m_OriginalScale = transform.localScale;
    }

    void Update ()
    {
        GetAudioBandDelta();
        m_NewScale = GetNewLocalScale();

        transform.localScale =
            Vector3.Lerp(transform.localScale, m_NewScale,
            m_NewScale.magnitude >= transform.localScale.magnitude ? 0.9f : 0.1f);
    }

    private void GetAudioBandDelta()
    {
        if (m_LeftChannel && !m_RightChannel)
            m_Frequency = m_AV.m_CurrentFrequencyLeft[m_AudioBand];
        else if (!m_LeftChannel && m_RightChannel)
            m_Frequency = m_AV.m_CurrentFrequencyRight[m_AudioBand];
        else
            m_Frequency = m_AV.m_CurrentFrequencyStereo[m_AudioBand];
    }

    private Vector3 GetNewLocalScale()
    {
        Vector3 newScale = Vector3.zero;

        newScale.x = Mathf.Clamp(m_ScaleX ? m_Frequency * m_MaxScale : 1, m_OriginalScale.x, m_MaxScale);
        newScale.y = Mathf.Clamp(m_ScaleY ? m_Frequency * m_MaxScale : 1, m_OriginalScale.y, m_MaxScale);
        newScale.z = Mathf.Clamp(m_ScaleZ ? m_Frequency * m_MaxScale : 1, m_OriginalScale.z, m_MaxScale);

        return newScale;
    }
}
