using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class QuizAnswerColor : MonoBehaviour
{
    public Color m_color;
    
    public void ColorizeAnswer()
    {
        gameObject.GetComponent<SpriteRenderer>().color = m_color;
    }
}
