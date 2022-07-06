using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WorldPhysics : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin channelPerlin;
    float shakeDur, intensity;
    bool shakeCamera;

    public bool stopPhysics;
    void Start()
    {
        stopPhysics = false;
        channelPerlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        channelPerlin.m_AmplitudeGain = 0;
    }
    void Update()
    {
        if (!stopPhysics)
        {
            Time.timeScale = 1;
        }
        if (stopPhysics)
        {
            Time.timeScale = 0;
        }
    }
    private void FixedUpdate()
    {
        //camera shake effect
        if (shakeCamera)
        {
            channelPerlin.m_AmplitudeGain = intensity;
            shakeDur -= Time.fixedDeltaTime;
            if (shakeDur <= 0)
            {
                channelPerlin.m_AmplitudeGain = 0;
                shakeCamera = false;
            }
        }
    }
    public void CameraShake(float duration, float intensity)
    {
        shakeDur = duration;
        this.intensity = intensity;
        shakeCamera = true;
    }
}
