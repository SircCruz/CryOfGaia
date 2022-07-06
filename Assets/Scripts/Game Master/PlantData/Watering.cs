using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watering : MonoBehaviour
{
    PlantData data;
    private void Start()
    {
        data = GameObject.Find("Plants").GetComponent<PlantData>();
        Destroy(gameObject, 2.9f);
    }
    private void OnDestroy()
    {
        data.wateringCan.enabled = true;
    }
}
