using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleEvent : MonoBehaviour
{
    public float m_cycleTime;

    public List<UnityEngine.Events.UnityEvent> m_events;

    private void Start()
    {
        StartCoroutine(CycleEvents());
    }

    public IEnumerator CycleEvents()
    {
        while (enabled)
        {
            foreach (UnityEngine.Events.UnityEvent action in m_events)
            {
                action.Invoke();
                yield return new WaitForSeconds(m_cycleTime);
            }
            yield return null;
        }
    }
}
