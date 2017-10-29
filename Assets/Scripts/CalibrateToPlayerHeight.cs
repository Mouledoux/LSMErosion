using UnityEngine;

public class CalibrateToPlayerHeight : MonoBehaviour
{
    [SerializeField]
    private float m_averageHeightMeters = 2f;

    [SerializeField] private float m_scaleMod = 1f;

    [SerializeField] private Vector3 m_originalLocalScale = Vector3.one;

	void Start ()
    {
        StartCoroutine(CalibrateHintCoroutine());
        //m_originalLocalScale = transform.localScale;
        //Calibrate();
	}

    [ContextMenu("Calibrate")]
    public void Calibrate()
    {
        transform.localScale = m_originalLocalScale * ((Valve.VR.InteractionSystem.Player.instance.eyeHeight / m_averageHeightMeters) * m_scaleMod);
    }

    public System.Collections.IEnumerator CalibrateHintCoroutine()
    {
        float prevBreakTime = Time.time;
        float prevHapticPulseTime = Time.time;

        yield return new WaitUntil(() => Valve.VR.InteractionSystem.Player.instance.handCount > 0);

        print(Valve.VR.InteractionSystem.Player.instance.handCount);

        while (true)
        {
            bool pulsed = false;

            //Show the hint on each eligible hand
            foreach (Valve.VR.InteractionSystem.Hand hand in Valve.VR.InteractionSystem.Player.instance.hands)
            {
                    Valve.VR.InteractionSystem.ControllerButtonHints.ShowTextHint(hand, Valve.VR.EVRButtonId.k_EButton_Grip, "Adjust Table Height");
                    prevBreakTime = Time.time;
                    prevHapticPulseTime = Time.time;

                if (Time.time > prevHapticPulseTime + 0.05f)
                {
                    //Haptic pulse for a few seconds
                    pulsed = true;

                    hand.controller.TriggerHapticPulse(500);
                }

                if(hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))
                {
                    Valve.VR.InteractionSystem.ControllerButtonHints.HideTextHint(hand, Valve.VR.EVRButtonId.k_EButton_Grip);
                    Valve.VR.InteractionSystem.ControllerButtonHints.HideTextHint(hand.otherHand, Valve.VR.EVRButtonId.k_EButton_Grip);
                    StopAllCoroutines();
                }

            }

            if (Time.time > prevBreakTime + 3.0f)
            {
                //Take a break for a few seconds
                yield return new WaitForSeconds(3.0f);

                prevBreakTime = Time.time;
            }

            if (pulsed)
            {
                prevHapticPulseTime = Time.time;
            }
            yield return null;
        }
    }
}
