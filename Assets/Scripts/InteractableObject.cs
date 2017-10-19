using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Material m_highlightMaterial;

    public UnityEngine.Events.UnityEvent m_onHighnight;
    public UnityEngine.Events.UnityEvent m_offHighnight;
    public UnityEngine.Events.UnityEvent m_onInteract;
    public UnityEngine.Events.UnityEvent m_offInteract;

    private Mouledoux.Components.Mediator.Subscriptions m_subscriptions = new Mouledoux.Components.Mediator.Subscriptions();

    private Mouledoux.Callback.Callback onHighlight;
    private Mouledoux.Callback.Callback offHighlight;
    private Mouledoux.Callback.Callback onInteract;
    private Mouledoux.Callback.Callback offInteract;

    private Renderer m_renderer;



    void Start()
    {
        m_renderer = GetComponentInChildren<Renderer>();

        onHighlight = OnHighlight;
        offHighlight = OffHighlight;

        onInteract = OnInteract;
        offInteract = OffInteract;

        m_subscriptions.Subscribe(gameObject.GetInstanceID().ToString() + "->onhighlight", onHighlight);
        m_subscriptions.Subscribe(gameObject.GetInstanceID().ToString() + "->offhighlight", offHighlight);

        m_subscriptions.Subscribe(gameObject.GetInstanceID().ToString() + "->oninteract", onInteract);
        m_subscriptions.Subscribe(gameObject.GetInstanceID().ToString() + "->offinteract", offInteract);
    }



    public void OnHighlight(Mouledoux.Callback.Packet packet)
    {
        m_onHighnight.Invoke();

        m_renderer.materials = new Material[] { m_renderer.materials[0], m_highlightMaterial };
    }

    public void OffHighlight(Mouledoux.Callback.Packet packet)
    {
        m_offHighnight.Invoke();

        m_renderer.materials = new Material[] { m_renderer.materials[0] };
    }

    public void OnInteract(Mouledoux.Callback.Packet packet)
    {
        m_onInteract.Invoke();
    }

    public void OffInteract(Mouledoux.Callback.Packet packet)
    {
        m_offInteract.Invoke();
    }
}
