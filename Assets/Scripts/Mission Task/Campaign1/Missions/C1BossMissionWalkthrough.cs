using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C1BossMissionWalkthrough : MonoBehaviour
{
    public ChainsawBoss boss;
    void OnEnable()
    {
        boss.enabled = !boss.enabled;
    }
    void OnDisable()
    {
        boss.enabled = !boss.enabled;
    }
}
