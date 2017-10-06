using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class _PLACEHOLDER_LAND_DEFORM : MonoBehaviour
{
    public GameObject sphere;
    private Mesh m_mesh;
    
	void Start ()
    {
        m_mesh = GetComponent<MeshFilter>().mesh;
        //Dictionary<GameObject, List<ref Vector3>> verticies = new Dictionary<GameObject, List<ref Vector3>>();

        foreach (Vector3 vert in m_mesh.vertices)
        {

        }

	}
	
	void Update ()
    {

	}
}
