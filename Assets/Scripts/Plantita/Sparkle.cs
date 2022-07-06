using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparkle : MonoBehaviour
{
    Animator m_animator;
    float dur, restoreDur;
    void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();
        m_animator.speed = Random.Range(0.6f, 1f);

        dur = 1.0f;
        restoreDur = dur;
    }
    private void FixedUpdate()
    {
        dur = Time.fixedDeltaTime;
        if(dur <= 0)
        {
            m_animator.speed = Random.Range(0.6f, 1f);
            dur = restoreDur;
        }
    }
}
