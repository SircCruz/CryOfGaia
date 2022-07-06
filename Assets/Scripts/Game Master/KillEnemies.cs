using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemies : MonoBehaviour
{
    Transform temp;
    void Start()
    {
        temp = GetComponent<Transform>();
    }
    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + 2f, transform.position.y);
        if(transform.position.x >= 50)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        transform.position = new Vector2(-41f, transform.position.y);
    }
}
