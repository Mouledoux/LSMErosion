using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    float graity = 9.81f;
    float speed = 0.0f;
    float time = 0.0f;
    float mod = 0.05f; 


	void Update ()
    {
        time += Time.deltaTime;
        speed = graity * time;
        speed *= mod;

        transform.Translate(Vector3.down * speed);
	}
}
