using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleEvent : MonoBehaviour
{
    public float m_cycleTime;
    public List<UnityEngine.Events.UnityEvent> m_events;
    public bool m_cycle;

    private void Start()
    {
        m_cycle = true;
        StartCoroutine(CycleEvents());
    }

    public IEnumerator CycleEvents()
    {
        while (m_cycle)
        {
            foreach (UnityEngine.Events.UnityEvent action in m_events)
            {
                action.Invoke();
                yield return new WaitForSeconds(m_cycleTime);
            }
            yield return null;
        }
    }

    public void StopCycle()
    {
        StopAllCoroutines();
    }
}
