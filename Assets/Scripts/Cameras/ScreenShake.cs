using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake Instance;

    private CinemachineImpulseSource cinemachineImpulseSourceShort;
    private CinemachineImpulseSource cinemachineImpulseSourceLong;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one ScreenShake in the scene! " + transform.position + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        cinemachineImpulseSourceShort = GetComponents<CinemachineImpulseSource>()[0];
        cinemachineImpulseSourceLong = GetComponents<CinemachineImpulseSource>()[1];
    }

    public void Shake(float intensity = 1f)
    {
        cinemachineImpulseSourceShort.GenerateImpulse(intensity);
    }

    public void Earthquake(float intensity = 1f)
    {
        cinemachineImpulseSourceLong.GenerateImpulse(intensity);
    }
}
