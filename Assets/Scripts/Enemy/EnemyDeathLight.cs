using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyDeathLight : MonoBehaviour
{
    Light2D enemyLight;

    private void Start()
    {
        enemyLight = gameObject.GetComponent<Light2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemyLight.intensity -= Time.fixedDeltaTime * 2;
        if (enemyLight.intensity <= 0)
        {
            enemyLight.enabled = false;
        }
    }
}
