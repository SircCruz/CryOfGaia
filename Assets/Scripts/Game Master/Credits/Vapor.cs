using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vapor : MonoBehaviour
{
    float speed = 0.5f;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + (speed * Time.fixedDeltaTime));
    }
}
