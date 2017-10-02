using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrateToPlayerHeight : MonoBehaviour
{
    private Vector3 m_originalLocalScale;

	void Start ()
    {
        m_originalLocalScale = transform.localScale;
        Calibrate();
	}

    
    public void Calibrate()
    {
        transform.localScale = m_originalLocalScale *
            Vector3.Distance(Valve.VR.InteractionSystem.Player.instance.transform.position, Camera.main.transform.position) / 2f;
    }
}
