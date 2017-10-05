using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    protected Mouledoux.Components.Mediator.Subscriptions m_subscriptions;
    protected Mouledoux.Callback.Callback onHighlight;
    protected Mouledoux.Callback.Callback offHighlight;
    protected Mouledoux.Callback.Callback onInteract;
    protected Mouledoux.Callback.Callback offInteract;


    // Use this for initialization
    protected void Start ()
    {
        m_subscriptions.Subscribe(GetInstanceID().ToString() + "->onhighlight", onHighlight);
        m_subscriptions.Subscribe(GetInstanceID().ToString() + "->offhighlight", offHighlight);
        m_subscriptions.Subscribe(GetInstanceID().ToString() + "->oninteract", onInteract);
        m_subscriptions.Subscribe(GetInstanceID().ToString() + "->offinteract", offInteract);
    }
}
