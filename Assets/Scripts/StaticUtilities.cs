using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticUtilities : MonoBehaviour
{
    public void TeleportObject(Transform objectTransform, Vector3 newGlobalPos)
    {
        objectTransform.position = newGlobalPos;
    }
}
