using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PLACEHOLDER_TOWER : MonoBehaviour
{
    public Material m_highlightMaterial;

    protected Mouledoux.Components.Mediator.Subscriptions m_subscriptions = new Mouledoux.Components.Mediator.Subscriptions();

    protected Mouledoux.Callback.Callback onHighlight;
    protected Mouledoux.Callback.Callback offHighlight;
    protected Mouledoux.Callback.Callback onInteract;
    protected Mouledoux.Callback.Callback offInteract;

    private Renderer m_renderer;

	// Use this for initialization
	void Start ()
    {
		m_renderer = GetComponentInChildren<Renderer>();

        onHighlight = OnHighlight;
        offHighlight = OffHighlight;

        m_subscriptions.Subscribe(gameObject.GetInstanceID().ToString() + "->onhighlight", onHighlight);
        m_subscriptions.Subscribe(gameObject.GetInstanceID().ToString() + "->offhighlight", offHighlight);
        m_subscriptions.Subscribe(gameObject.GetInstanceID().ToString() + "->oninteract", onInteract);
        m_subscriptions.Subscribe(gameObject.GetInstanceID().ToString() + "->offinteract", offInteract);
    }

    public void OnHighlight(Mouledoux.Callback.Packet packet)
    {
        m_renderer.materials = new Material[] { m_renderer.materials[0], m_highlightMaterial };
    }

    public void OffHighlight(Mouledoux.Callback.Packet packet)
    {
        m_renderer.materials = new Material[] { m_renderer.materials[0] };
    }
}
