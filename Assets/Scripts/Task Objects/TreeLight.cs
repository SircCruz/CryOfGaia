using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TreeLight : MonoBehaviour
{
    Light2D treeLight;
    TreeHP treeHP;

    float maxLight = 1.3f;
    float minLight = 0.3f;
    float speed = 0.85f;

    float hurtDur = 0.5f;

    bool isHurt;
    bool lightenUp;

    private void Start()
    {
        treeLight = gameObject.GetComponent<Light2D>();
        treeHP = gameObject.GetComponentInParent<TreeHP>();
    }
    private void FixedUpdate()
    {
        if(treeHP.healthPoints <= 0)
        {
            Destroy(gameObject);
        }
        if (!isHurt)
        {
            UpdatePlayingSpeed();
        }
        if(!lightenUp)
        {
            treeLight.intensity += Time.fixedDeltaTime * speed;
            if(treeLight.intensity >= maxLight)
            {
                lightenUp = true;
            }
        }
        if (lightenUp)
        {
            treeLight.intensity -= Time.fixedDeltaTime * speed;
            if (treeLight.intensity <= minLight)
            {
                lightenUp = false;
            }
        }
        if (isHurt)
        {
            speed = 10f;
            hurtDur -= Time.fixedDeltaTime;
            if(hurtDur <= 0)
            {
                isHurt = false;
                hurtDur = 0.5f;
                UpdatePlayingSpeed();
            }
        }
    }
    public void PlayHurtLight()
    {
        isHurt = true;
    }
    void UpdatePlayingSpeed()
    {
        if (treeHP.healthPoints >= 1 && treeHP.healthPoints <= 50)
        {
            speed = 6f;
        }
        else if (treeHP.healthPoints >= 51 && treeHP.healthPoints <= 100)
        {
            speed = 3f;
        }
        else
        {
            speed = 0.85f;
        }
    }
}
