using UnityEngine;

public class CalibrateToPlayerHeight : MonoBehaviour
{
    [SerializeField]
    private float m_averageHeightMeters = 2f;

    [SerializeField] private float m_scaleMod = 1f;

    [SerializeField] private Vector3 m_originalLocalScale = Vector3.one;

	void Start ()
    {
        m_originalLocalScale = transform.localScale;
        //Calibrate();
	}

    [ContextMenu("Calibrate")]
    public void Calibrate()
    {
        transform.localScale = m_originalLocalScale * ((Valve.VR.InteractionSystem.Player.instance.eyeHeight / m_averageHeightMeters) * m_scaleMod);
    }
}
