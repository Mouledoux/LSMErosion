using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutModel : MonoBehaviour
{
	void Start ()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Vector3[] verts = mf.mesh.vertices;
        Color[] colors = new Color[verts.Length];

        int h, l;
        h = l = 0;

        for (int i = 0; i < verts.Length; i++)
        {
            

            int floor = Mathf.FloorToInt(verts[i].y);
            floor += floor % 10;
            verts[i].y = floor;

            h = floor > h ? floor : h;
            l = floor < l ? floor : l;

            colors[i].r = Mathf.InverseLerp(h, l, floor);
            colors[i].g = Mathf.InverseLerp(l, h, floor);
            colors[i].b = 0f;
            colors[i].a = 1f;
        }

        mf.mesh.vertices = verts;
        mf.mesh.colors = colors;

    }
}
