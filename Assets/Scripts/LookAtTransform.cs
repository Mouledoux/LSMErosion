using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTransform : MonoBehaviour
{
    public Transform m_target;

    public enum AXIS
    {
    }

	void Update ()
    {
        transform.LookAt(m_target);
        //transform.up = Vector3.up;   
	}
}
