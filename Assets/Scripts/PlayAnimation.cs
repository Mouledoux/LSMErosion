using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour
{
    Animation anim;

    void Start ()
    {
        anim = gameObject.GetComponent<Animation>();
	}
	
    [ContextMenu("Test")]
	public void PlayTheAnimation()
    {
        anim.Play();
    }

}
