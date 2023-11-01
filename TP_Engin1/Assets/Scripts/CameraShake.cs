using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera m_camera;

    [SerializeField]
    private float m_duration;

    [SerializeField]
    private AnimationCurve m_amplitudeGain;

    [SerializeField]
    private AnimationCurve m_frequencyGain;

    private float m_actualDuration;
    private CinemachineBasicMultiChannelPerlin m_cmBMCP;

    private void Awake()
    {
        m_cmBMCP = m_camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (m_actualDuration > 0) 
        {
            m_actualDuration -= Time.deltaTime;
            m_cmBMCP.m_AmplitudeGain = m_amplitudeGain.Evaluate(m_actualDuration/ m_duration);
            m_cmBMCP.m_FrequencyGain = m_frequencyGain.Evaluate(m_actualDuration/ m_duration);
        }
    }

    public void ShakeCamera() 
    {
        m_actualDuration = m_duration;
    }




}
