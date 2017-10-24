using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTransform : MonoBehaviour
{
    public Transform m_target;
    public AXIS m_axis;

    public enum AXIS
    {
        X, Y, Z
    }

	void Update ()
    {
		switch(m_axis)
        {
            case AXIS.X:
                transform.LookAt(m_target, transform.up);
                //transform.right = -(transform.position - m_target.position).normalized;
                break;
            case AXIS.Y:
                //transform.up = -(transform.position - m_target.position).normalized;
                break;
            case AXIS.Z:
                transform.LookAt(m_target, transform.up);
                //transform.forward = -(transform.position - m_target.position).normalized;
                break;
        }
	}
}
