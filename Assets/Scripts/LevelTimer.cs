using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public float m_time;
    private float m_timer = 0;

    public UnityEngine.UI.Text m_countDownClock;
    public Transform m_progressBar;
    public RectTransform m_progressBarUI;

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

        float reaminingTime = (m_time - m_timer);
        int min = (int)(reaminingTime / 60f);
        int sec = (int)(reaminingTime % 60f);
        m_countDownClock.text = min.ToString("00") + ":" + sec.ToString("00");
        m_progressBar.localScale = new Vector3(1, m_timer / m_time, 1);
        m_progressBarUI.localScale = new Vector3(1, m_timer / m_time, 1);

        if (m_timer >= m_time)
        {
            m_onEnd.Invoke();
            enabled = false;
        }
	}
}
