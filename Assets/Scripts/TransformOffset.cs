using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformOffset : MonoBehaviour
{
    public GameObject m_Target;
    [SerializeField]
    private Vector3 m_Offset = Vector3.zero;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = m_Target.transform.position + m_Offset;
	}
}
