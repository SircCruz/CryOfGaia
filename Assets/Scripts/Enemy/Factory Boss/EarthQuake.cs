using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EarthQuake : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin perlin;

    public Transform[] platform;

    private float[] targetPosY;
    private float[] transistor;

    private float speed = 6f;

    private bool[] isMoving;

    private void OnEnable()
    {
        perlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        targetPosY = new float[platform.Length];
        transistor = new float[platform.Length];

        isMoving = new bool[platform.Length];

        for(int i = 0; i < targetPosY.Length; i++)
        {
            transistor[i] = platform[i].transform.position.y;
            targetPosY[i] = Random.Range(-6f, 0);
        }
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < platform.Length; i++)
        {
            if(targetPosY[i] < -3f)
            {
                if(platform[i].transform.position.y > targetPosY[i])
                {
                    transistor[i] += Time.fixedDeltaTime * -speed;
                    isMoving[i] = true;
                }
                else
                {
                    isMoving[i] = false;
                }
            }
            else
            {
                if (platform[i].transform.position.y < targetPosY[i])
                {
                    transistor[i] += Time.fixedDeltaTime * speed;
                    isMoving[i] = true;
                }
                else
                {
                    isMoving[i] = false;
                }
            }
            platform[i].position = new Vector2(platform[i].transform.position.x, transistor[i]);
        }
        MoveCheck();
    }
    private void MoveCheck()
    {
        if(isMoving[0] || isMoving[1] || isMoving[2] || isMoving[3])
        {
            perlin.m_AmplitudeGain = 8;
        }
        else
        {
            perlin.m_AmplitudeGain = 0;
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        perlin.m_AmplitudeGain = 0;
    }
}
