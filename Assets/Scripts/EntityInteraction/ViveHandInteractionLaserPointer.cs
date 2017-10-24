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
        return (m_hand.GetStandardInteractionButtonDown());
    }


    // ---------- ---------- ---------- ---------- ----------
    public int ObjectInteract()
    {
        InteractableObject io = m_targetObject.GetComponent<InteractableObject>();

        Mouledoux.Components.Mediator.instance.NotifySubscribers
            (m_targetObject.GetInstanceID().ToString() + "->oninteract", new Mouledoux.Callback.Packet());

        if (io.m_interactionType == InteractableObject.InteractionType.PICKUP && !io.m_lockedInPlace)
        {
            if (m_isHoldingSomething) return -1;

            StartCoroutine(HoldObject(m_raycast.transform.gameObject));
        }
        
        else if (io.m_interactionType == InteractableObject.InteractionType.LONGINTERACT)
        {
            StartCoroutine(LongInteract(m_raycast.transform.gameObject));
        }

        return 0;
    }


    // ---------- ---------- ---------- ---------- ----------
    public void UpdateLaser()
    {
        m_lineRenderer.SetPositions( new Vector3[] {m_hand.transform.position, m_raycast.point});
    }


    // ---------- ---------- ---------- ---------- ----------
    public System.Collections.IEnumerator HoldObject(GameObject go)
    {
        Vector3 lastPos = Vector3.zero;

        Collider c = m_raycast.collider;

        c.enabled = false;
        m_isHoldingSomething = true;

        while (m_hand.GetStandardInteractionButton() || !go.CompareTag(m_raycast.transform.tag) || m_raycast.transform.GetComponent<InteractableObject>())
        {
            go.transform.position = m_raycast.point;
            yield return null;
        }

        go.transform.parent = m_raycast.transform;
        
        Mouledoux.Components.Mediator.instance.NotifySubscribers
            (go.GetInstanceID().ToString() + "->offinteract", new Mouledoux.Callback.Packet());

        c.enabled = true;
        m_isHoldingSomething = false;
    }


    public System.Collections.IEnumerator LongInteract(GameObject go)
    {
        yield return new WaitWhile(() => (m_hand.GetStandardInteractionButton()));
;
        Mouledoux.Components.Mediator.instance.NotifySubscribers
            (go.GetInstanceID().ToString() + "->offinteract", new Mouledoux.Callback.Packet());

        Mouledoux.Components.Mediator.instance.NotifySubscribers
            (m_raycast.transform.gameObject.GetInstanceID().ToString() + "->offinteract", new Mouledoux.Callback.Packet());
    }
}
