using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAutoRestart : MonoBehaviour
{
    [SerializeField]
    float timeToIdle;
    float cTime;

	// Use this for initialization
	void Start ()
    {
        cTime = 0;
	}

    // Update is called once per frame
    void Update()
    {
        if (Valve.VR.OpenVR.System.GetTrackedDeviceActivityLevel(0) == Valve.VR.EDeviceActivityLevel.k_EDeviceActivityLevel_Idle ||
            Valve.VR.OpenVR.System.GetTrackedDeviceActivityLevel(0) == Valve.VR.EDeviceActivityLevel.k_EDeviceActivityLevel_Standby ||
            Valve.VR.OpenVR.System.GetTrackedDeviceActivityLevel(0) == Valve.VR.EDeviceActivityLevel.k_EDeviceActivityLevel_UserInteraction_Timeout)
        {
            cTime += Time.deltaTime;
        }

        if (Valve.VR.OpenVR.System.GetTrackedDeviceActivityLevel(0) == Valve.VR.EDeviceActivityLevel.k_EDeviceActivityLevel_UserInteraction ||
            Valve.VR.OpenVR.System.GetTrackedDeviceActivityLevel(0) == Valve.VR.EDeviceActivityLevel.k_EDeviceActivityLevel_Unknown)
        {
            cTime = 0;
        }


        if(cTime >= timeToIdle)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

    }
}
