using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformOffset : MonoBehaviour
{
    public GameObject m_Target;
    Vector3 m_Offset;

	// Use this for initialization
	void Start ()
    {
        m_Offset = transform.position - m_Target.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = m_Target.transform.position;
	}
}
