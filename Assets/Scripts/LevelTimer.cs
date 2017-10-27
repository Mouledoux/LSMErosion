using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public float m_time;
    private float m_timer = 0;

    public UnityEngine.UI.Text m_countDownClock;

    public UnityEngine.Events.UnityEvent m_onStart;
    public UnityEngine.Events.UnityEvent m_onEnd;

    // Use this for initialization
    void Start ()
    {
        m_onStart.Invoke();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_timer += Time.deltaTime;

        m_countDownClock.text = (m_time - m_timer).ToString("0");

        if (m_timer >= m_time)
        {
            m_onEnd.Invoke();
            enabled = false;
        }
	}
}
