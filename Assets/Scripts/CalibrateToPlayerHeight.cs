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

    
    public void Calibrate()
    {
        transform.localScale = m_originalLocalScale *
           ( Vector3.Distance(Valve.VR.InteractionSystem.Player.instance.transform.position, Camera.main.transform.position) / m_averageHeightMeters ) * m_scaleMod;
    }
}
