using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateScoreCard : MonoBehaviour
{
    [SerializeField] AnimationCurve m_growthAnimation;
    [SerializeField] float m_animateTime = 0;
    [SerializeField] Text m_scoretext;
    [SerializeField] List<GameObject> m_Fireworks;

    [ContextMenu("Animate")]
    public void _test()
    {
        AnimScoreCard("Congratulation \n\n\n[Insert title]\n\n\nYou have protected the coast");
    }


    public void AnimScoreCard(string s)
    {
        m_scoretext.text = s;
        foreach(GameObject g in m_Fireworks)
        {
            g.SetActive(true);
        }

        StartCoroutine(AnimateScore());
    }


    public IEnumerator AnimateScore()
    {
        float t = 0;
        while(t <= m_animateTime)
        {
            Vector3 v = Vector3.zero;
            v.x = v.y = v.z = m_growthAnimation.Evaluate(t/m_animateTime);
            
            gameObject.transform.localScale = v;

            t += Time.deltaTime;

            yield return null;
        }
    }
}
