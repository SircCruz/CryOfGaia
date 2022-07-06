using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    public bool isFailed;
    public Transform character;
    public CinemachineVirtualCamera vcam;
    CinemachineFramingTransposer composer;

    [SerializeField] private float leftEdge;
    [SerializeField] private float rightEdge;

    private void Start()
    {
        composer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
        composer.m_DeadZoneWidth = 0f;
    }
    void Update()
    {
        if (!isFailed)
        {
            if (character.transform.position.x >= rightEdge)
            {
                composer.m_DeadZoneWidth = 1;
            }
            else if (character.transform.position.x <= leftEdge)
            {
                composer.m_DeadZoneWidth = 1;
            }
            else
            {
                composer.m_DeadZoneWidth = 0;
            }
        }
        if (isFailed)
        {
            composer.m_DeadZoneWidth = 0;
        }
    }
}
