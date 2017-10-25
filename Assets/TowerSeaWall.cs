using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSeaWall : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(iSnapToLand());
    }

    private void OnTriggerEnter(Collider other)
    {
        RaycastHit raycast;
        Physics.Raycast(transform.position, -transform.up, out raycast);

        if (!raycast.transform.CompareTag(tag)) StartCoroutine(iDestroy());
    }

    public IEnumerator iDestroy()
    {
        Vector3 nPos = transform.localPosition;
        nPos.y *= -5;

        float timer = 1;

        while (timer > 0)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, nPos, Time.deltaTime);
            timer -= Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);
    }

    public IEnumerator iSnapToLand()
    {
        Vector3 rayPos = transform.position;
        rayPos.y *= 1.01f;

        RaycastHit raycast;
        Vector3 shoreLine = transform.position + (transform.forward * 0.01f);
        Physics.Raycast(rayPos, (shoreLine - rayPos).normalized, out raycast);


        Debug.DrawLine(rayPos, raycast.point, Color.red, 10f);

        while(raycast.transform.CompareTag(tag))
        {
            transform.position = raycast.point;
            rayPos = transform.position;
            rayPos.y *= 1.01f;
            shoreLine = transform.position + (transform.forward * 0.01f);
            Physics.Raycast(rayPos, (shoreLine - rayPos).normalized, out raycast);

            yield return null;
        }
    }
}
