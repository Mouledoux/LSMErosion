using UnityEngine;

[RequireComponent(typeof(Valve.VR.InteractionSystem.Hand), typeof(LineRenderer))]
public class ViveHandInteractionLaserPointer : MonoBehaviour
{
    [SerializeField]
    private string m_hitTag;

    private GameObject m_targetObject;

    private RaycastHit m_raycast;

    private Valve.VR.InteractionSystem.Hand m_hand;
    private LineRenderer m_lineRenderer;

	void Start ()
    {
        m_hand = GetComponent<Valve.VR.InteractionSystem.Hand>();
        m_lineRenderer = GetComponent<LineRenderer>();
	}

	void Update ()
    {
        if (CheckObjectHit())
        {
            Mouledoux.Components.Mediator.instance.NotifySubscribers
                (m_raycast.transform.gameObject.GetInstanceID().ToString() + "->onhighlight", new Mouledoux.Callback.Packet());

            if (CheckObject())
            {
                if (CheckInput())
                {
                    Mouledoux.Components.Mediator.instance.NotifySubscribers
                        (m_raycast.transform.gameObject.GetInstanceID().ToString() + "->oninteract", new Mouledoux.Callback.Packet());

                    PickUpObject();
                }
            }
        }

        UpdateLaser();
    }


    public bool CheckObjectHit()
    {
        return (Physics.Raycast(m_hand.transform.position, m_hand.transform.forward, out m_raycast));
    }

    public bool CheckObject()
    {
        return m_raycast.transform.gameObject.CompareTag(m_hitTag);
    }

    public bool CheckInput()
    {
        return (m_hand.controller.GetHairTriggerDown());
    }

    public int PickUpObject()
    {
        StartCoroutine(HoldObject());
        return 0;
    }

    public void UpdateLaser()
    {
        m_lineRenderer.SetPositions( new Vector3[] {m_hand.transform.position, m_raycast.point});
    }

    public System.Collections.IEnumerator HoldObject()
    {
        Transform t = m_raycast.transform;
        Collider c = m_raycast.collider;

        c.enabled = false;

        while(m_hand.controller.GetHairTrigger())
        {
            t.position = m_raycast.point + (transform.up * 0.01f);
            yield return null;
        }

        c.enabled = true;
    }
}
