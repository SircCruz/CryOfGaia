using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGripAnim : MonoBehaviour
{
    private bool isRotating;

    private void FixedUpdate()
    {
        if (isRotating)
        {
            transform.Rotate(new Vector3(0, 0, -360 * (Time.fixedDeltaTime * 0.3f)));
        }
    }
    public void RotateGripMessage()
    {
        isRotating = true;
    }
    public void RotateGripMessageExit()
    {
        isRotating = false;
    }
}
