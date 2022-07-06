using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C1Mission2Walkthrough : MonoBehaviour
{
    public C1Mission2 mission;
    void OnEnable()
    {
        mission.enabled = !mission.enabled;
    }
    void OnDisable()
    {
        mission.enabled = !mission.enabled;
    }
}
