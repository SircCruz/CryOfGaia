using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorailKillCounter : MonoBehaviour
{
    TutorialTask task;

    private void Start()
    {
        task = GameObject.Find("Task Monitor").GetComponent<TutorialTask>();
    }
    private void OnDestroy()
    {
        task.AddValue();
    }
}
