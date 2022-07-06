using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossCharacterDamageUI : MonoBehaviour
{
    private float duration = 1.5f;
    private float vaporSpeed = 1f;

    C1BossMissionPlayerReact player;
    TextMeshProUGUI damageUI;

    float xPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<C1BossMissionPlayerReact>();
        damageUI = gameObject.GetComponent<TextMeshProUGUI>();
        damageUI.text = player.damage;

        xPos = Random.Range(-0.2f, 0.2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x + (xPos * Time.fixedDeltaTime), transform.position.y + (vaporSpeed * Time.fixedDeltaTime));

        if (player.transform.localScale.x == -0.8f)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, 1);
        }

        duration -= Time.fixedDeltaTime;
        if (duration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
