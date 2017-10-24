using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerJettie : MonoBehaviour
{
    public float m_growthRate;

    [SerializeField]
    private float m_offset;

    private RaycastHit m_raycast;


    private void Start()
    {
        StartCoroutine(GrowLand());
    }


    public IEnumerator SnapToLand()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(1 - m_growthRate);
        }
    }

    public IEnumerator GrowLand()
    {

        while (enabled)
        {
            Vector3 rayPos = transform.position + transform.InverseTransformDirection(Vector3.right);
            Physics.Raycast(rayPos, -transform.forward, out m_raycast);

            Debug.DrawLine(rayPos, m_raycast.point, Color.red);//, 1);

            yield return null;// new WaitForSeconds(1 - m_growthRate);
        }
    }
}
