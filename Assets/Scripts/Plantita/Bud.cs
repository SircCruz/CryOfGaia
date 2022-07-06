using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bud : MonoBehaviour
{
    public bool hide;
    private void Update()
    {
        if (hide)
        {
            Destroy(gameObject);
        }
    }
}
