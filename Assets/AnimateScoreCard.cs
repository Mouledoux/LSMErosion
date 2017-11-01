using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimateScoreCard : MonoBehaviour
{
    [SerializeField] AnimationCurve m_growthAnimation;
    [SerializeField] float m_animateTime = 0;
    [SerializeField] Text m_scoretext;

    [ContextMenu("Animate")]
    public void _test()
    {
        StartCoAnim("Congratulation \n\n\n[Insert title]\n\n\nYou have protected the coast");
    }


    public void StartCoAnim(string s)
    {
        m_scoretext.text = s;
        StartCoroutine(AnimateScore());
    }


    public IEnumerator AnimateScore()
    {
        float t = 0;
        while(t <= m_animateTime)
        {
            Vector3 v = Vector3.zero;
            v.x = v.y = m_growthAnimation.Evaluate(t/m_animateTime);
            gameObject.transform.localScale = v;

            t += Time.deltaTime;

            yield return null;
        }
    }
}
