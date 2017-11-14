using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PLACEHOLDER_TOWER : InteractableObject
{
	private void Start ()
    {
        //base.Start();
    }

    new protected void OnHighlight(Mouledoux.Callback.Packet packet)
    {
        base.OnHighlight(packet);
    }

    new protected void OffHighlight(Mouledoux.Callback.Packet packet)
    {
        base.OffHighlight(packet);
    }

    new protected void OnInteract(Mouledoux.Callback.Packet packet)
    {
        base.OnInteract(packet);
    }

    new protected void OffInteract(Mouledoux.Callback.Packet packet)
    {
        base.OffInteract(packet);
    }
}
