using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateScoreCard : MonoBehaviour
{
    [SerializeField] AnimationCurve m_growthAnimation;
    [SerializeField] float m_animateTime = 0;

    IEnumerator AnimateScore()
    {
        float t = 0;
        while(t < m_animateTime)
        {
            t += Time.deltaTime;
        }
        return null;
    }
}
