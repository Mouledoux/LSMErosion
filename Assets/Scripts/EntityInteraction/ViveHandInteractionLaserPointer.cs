using UnityEngine;

[RequireComponent(typeof(Valve.VR.InteractionSystem.Hand), typeof(LineRenderer))]
public class ViveHandInteractionLaserPointer : MonoBehaviour
{
    private GameObject m_targetObject;
    private bool m_isHoldingSomething = false;

    private RaycastHit m_raycast;

    private Valve.VR.InteractionSystem.Hand m_hand;
    private LineRenderer m_lineRenderer;



    // ---------- ---------- ---------- ---------- ----------
    void Start ()
    {
        m_hand = GetComponent<Valve.VR.InteractionSystem.Hand>();
        m_lineRenderer = GetComponent<LineRenderer>();
	}


    // ---------- ---------- ---------- ---------- ----------
    void Update ()
    {
        if (CheckObjectHit())
        {
            if (CheckObject())
            {
                if (CheckInput())
                {
                    ObjectInteract();
                }
            }
        }

        UpdateLaser();
    }


    // ---------- ---------- ---------- ---------- ----------
    public bool CheckObjectHit()
    {
        if (Physics.Raycast(m_hand.transform.position, m_hand.transform.forward, out m_raycast))
        {
            if (m_targetObject != m_raycast.transform.gameObject && !m_isHoldingSomething)
            {
                if (m_targetObject != null)
                {
                    Mouledoux.Components.Mediator.instance.NotifySubscribers
                        (m_targetObject.GetInstanceID().ToString() + "->offhighlight", new Mouledoux.Callback.Packet());
                }

                m_targetObject = m_raycast.transform.gameObject;

                Mouledoux.Components.Mediator.instance.NotifySubscribers
                    (m_targetObject.GetInstanceID().ToString() + "->onhighlight", new Mouledoux.Callback.Packet());
            }

            return true;
        }

        m_raycast.point = m_hand.transform.position + m_hand.transform.forward;
        return false;
    }


    // ---------- ---------- ---------- ---------- ----------
    public bool CheckObject()
    {
        return m_targetObject.GetComponent<InteractableObject>();
    }


    // ---------- ---------- ---------- ---------- ----------
    public bool CheckInput()
    {
        return (m_hand.GetStandardInteractionButtonDown());//controller.GetHairTriggerDown());
    }


    // ---------- ---------- ---------- ---------- ----------
    public int ObjectInteract()
    {
        Mouledoux.Components.Mediator.instance.NotifySubscribers
            (m_targetObject.GetInstanceID().ToString() + "->oninteract", new Mouledoux.Callback.Packet());

        if (m_targetObject.GetComponent<InteractableObject>().m_pickup)
        {
            StartCoroutine(HoldObject());
        }


        return 0;
    }


    // ---------- ---------- ---------- ---------- ----------
    public void UpdateLaser()
    {
        m_lineRenderer.SetPositions( new Vector3[] {m_hand.transform.position, m_raycast.point});
    }


    // ---------- ---------- ---------- ---------- ----------
    public System.Collections.IEnumerator HoldObject()
    {
        Vector3 lastPos = Vector3.zero;

        Transform t = m_raycast.transform;
        Collider c = m_raycast.collider;

        c.enabled = false;
        m_isHoldingSomething = true;

        while (m_hand.GetStandardInteractionButton())//controller.GetHairTrigger())
        {
            t.position = m_raycast.point;
            yield return null;
        }

        t.parent = m_raycast.transform;
        
        Mouledoux.Components.Mediator.instance.NotifySubscribers
            (t.gameObject.GetInstanceID().ToString() + "->offinteract", new Mouledoux.Callback.Packet());

        c.enabled = true;
        m_isHoldingSomething = false;
    }
}
