using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationFall : MonoBehaviour
{

    private void FixedUpdate()
    {
        RaycastHit rayHit;
        if(Physics.Raycast(transform.position, -transform.forward, out rayHit))
        {
            if (rayHit.transform.CompareTag("Water"))
            {
                Destroy(gameObject);
            }
        }
    }
}
