using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeOnStart : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent onStart;

	void Start ()
    {
        onStart.Invoke();
	}

}
