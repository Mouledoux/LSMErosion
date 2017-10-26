using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public static string[] m_answers;

	// Use this for initialization
	void Start ()
    {
		m_answers = new string[5];
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public string[] GetAnswers()
    {
        return m_answers;
    }
}
