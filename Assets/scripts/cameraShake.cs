using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class cameraShake : MonoBehaviour
{
    public float shakeDuration = 0.3f;
    public float shakeAmplitude = 1.2f;
    public float shakeFrequency = 2.0f;

    private float shakeElapsedTime = 0f;

    public CinemachineVirtualCamera virtualCamera;
    public CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    public static cameraShake Instance;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        // Get Virtual Camera Noise Profile
        if (virtualCamera != null)
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the Cinemachine componet is not set, avoid update
        if (virtualCamera != null && virtualCameraNoise != null)
        {

            // If Camera Shake effect is still playing
            if (shakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = shakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = shakeFrequency;

                // Update Shake Timer
                shakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                shakeElapsedTime = 0f;
            }
        }
    }

    public void CamShake()
    {
        shakeElapsedTime = shakeDuration;
    }
    public void IntroShake()
    {
        shakeElapsedTime = 3;
    }
}