using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private Mouledoux.Components.Mediator.Subscriptions m_subscriptions;
    private Mouledoux.Callback.Callback onHighlight;
    private Mouledoux.Callback.Callback offHighlight;
    private Mouledoux.Callback.Callback onInteract;
    private Mouledoux.Callback.Callback offInteract;


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
