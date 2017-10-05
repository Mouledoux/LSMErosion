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
            if(CheckObject())
                if(CheckInput())
                    PickUpObject();

        UpdateLaser();
    }


    public bool CheckObjectHit()
    {
        return (Physics.Raycast(m_hand.transform.position, m_hand.transform.forward, out m_raycast));

        if (!Physics.Raycast(m_hand.transform.position, m_hand.transform.forward, out m_raycast)) return false;
        else if (!m_raycast.transform.gameObject.GetComponent<Valve.VR.InteractionSystem.Interactable>()) return false;
        else if (!m_raycast.transform.gameObject.CompareTag(m_hitTag)) return false;

        else
        {
            Mouledoux.Components.Mediator.instance.
                NotifySubscribers(m_raycast.transform.gameObject.GetInstanceID().ToString(), new Mouledoux.Callback.Packet());
            return true;
        }
    }

    public bool CheckObject()
    {
        return m_raycast.transform.gameObject.CompareTag(m_hitTag);
    }

    public void UpdateLaser()
    {
        Vector3 endPoint = CheckObjectHit() ? m_raycast.point : m_hand.transform.position + m_hand.transform.forward;
        
        Vector3[] v = { m_hand.transform.position, endPoint };
        m_lineRenderer.SetPositions(v);

        if (m_hand.AttachedObjects.Count > 1)
            m_hand.AttachedObjects[1].attachedObject.transform.position = endPoint;
    }


    public bool CheckInput()
    {
        return (m_hand.controller.GetHairTriggerDown());
    }


    public int PickUpObject()
    {
        //m_hand.AttachObject(m_raycast.transform.gameObject);
        StartCoroutine(HoldObject(m_raycast.transform.gameObject));
        return 0;
    }

    public System.Collections.IEnumerator HoldObject(GameObject target)
    {
        target.GetComponent<Collider>().enabled = false;
        while(m_hand.controller.GetHairTrigger())
        {
            target.transform.position = m_raycast.point;
            yield return null;
        }
        target.GetComponent<Collider>().enabled = true;
    }
}
