using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceTeleport : MonoBehaviour
{
    public UnityEngine.UI.Image blindfold;
    public float fadeTime;


    public void TeleportPlayerToTransform(Transform pos)
    {
        StartCoroutine(FadeInOut());

        Valve.VR.InteractionSystem.Player.instance.transform.position = pos.position;
        Valve.VR.InteractionSystem.Player.instance.transform.rotation = pos.rotation;
    }

    public IEnumerator FadeInOut()
    {
        Color c = blindfold.color;

        c.a = 1f;
        blindfold.color = c;

        while (blindfold.color.a > 0.01f)
        {
            c.a -= (Time.deltaTime / fadeTime);
            blindfold.color = c;
            yield return null;
        }

        c.a = 0f;
        blindfold.color = c;
    }
}
