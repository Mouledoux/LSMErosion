using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class TriggerAnimation : MonoBehaviour
{
    [SerializeField]
    private List<AnimationClip> m_animations;

    private Animation anim;

    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        
        foreach(AnimationClip ac in m_animations)
        {
            if (!ac.legacy)
                Debug.LogError("The \"" + ac.name + "\"" + " Animation Clip is not marked as Legacy. \nThis may cause issues with Script TriggerAnimation.cs on the following GameObject: " + gameObject.name);

            anim.AddClip(ac, ac.name);
            //print(ac.name);
        }
    }

    public void PlayAnimationAtIndex(int i)
    {
        anim.clip = m_animations[i];
        anim.Play();
    }
}
