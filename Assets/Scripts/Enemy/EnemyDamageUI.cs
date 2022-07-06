using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyDamageUI : MonoBehaviour
{
    private float duration = 1.5f;
    private float vaporSpeed = 1f;

    TextMeshProUGUI damageText;
    Chainsaw chainsaw; 

    float xPos;
    void Start()
    {
        damageText = GetComponent<TextMeshProUGUI>();
        chainsaw = GetComponentInParent<Chainsaw>();

        damageText.text = chainsaw.damage;

        xPos = Random.Range(-0.2f, 0.2f);
    }
    private void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + (xPos * Time.fixedDeltaTime), transform.position.y + (vaporSpeed * Time.fixedDeltaTime));

        if (chainsaw.transform.localScale.x == -1)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }

        duration -= Time.fixedDeltaTime;
        if(duration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
