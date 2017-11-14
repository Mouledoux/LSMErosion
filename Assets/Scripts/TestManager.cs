using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public static string[] m_answers;
    public static bool[] m_correctAnswer;

	// Use this for initialization
	void Start ()
    {
		m_answers = new string[5];
        m_correctAnswer = new bool[5];
        for (int i = 0; i < m_answers.Length; ++i)
        {
            m_answers[i] = null;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AddAnswer(string answer)
    {
        for (int i = 0; i < m_answers.Length; ++i)
        {
            if(m_answers[i] == null)
            {
                m_answers[i] = answer.Substring(1).ToLower();

                m_correctAnswer[i] = answer[0] == '1' ? true : false;
                return;
            }
        }
    }

    public string[] GetAnswers()
    {
        return m_answers;
    }

    public string GetScore()
    {
        int score = 0;
        for(int i = 0; i < m_correctAnswer.Length; i++)
        {
            score += m_correctAnswer[i] ? 1 : 0;
        }

        return score.ToString() + "/" + m_correctAnswer.Length.ToString();
    }
}
