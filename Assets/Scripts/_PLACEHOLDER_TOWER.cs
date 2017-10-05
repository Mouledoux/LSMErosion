using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PLACEHOLDER_TOWER : InteractableObject
{
    public Material m_highlightMaterial;

    private Renderer m_renderer;

	// Use this for initialization
	new void Start ()
    {
		m_renderer = GetComponentInChildren<Renderer>();

        base.Start();
	}

    public void OnHighlight(Mouledoux.Callback.Packet packet)
    {
        m_renderer.materials[1] = m_highlightMaterial;
    }

    public void OffHighlight(Mouledoux.Callback.Packet packet)
    {
        m_renderer.materials[1] = null;
    }
}
