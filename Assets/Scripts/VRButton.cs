using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Valve.VR.InteractionSystem.Interactable))]
public class VRButton : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent OnClick;
    public string actionName;

    private Valve.VR.InteractionSystem.Hand m_hand;

    private void Start()
    {
        m_hand = GetComponent<Valve.VR.InteractionSystem.Hand>();
    }

    private void HandHoverUpdate()
    {
        Valve.VR.InteractionSystem.ControllerButtonHints.ShowTextHint(m_hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger, actionName);

        if (m_hand.GetStandardInteractionButton())
        {
            OnClick.Invoke();
        }
    }

    private void OnHandHoverEnd()
    {
        Valve.VR.InteractionSystem.ControllerButtonHints.HideTextHint(m_hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
    }

    private void OnDisable()
    {
        Valve.VR.InteractionSystem.ControllerButtonHints.HideTextHint(m_hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
    }
}
