using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C1BossMissionPlayerReact : MonoBehaviour
{
    Player player;
    Rigidbody2D rgbd;

    bool isHurt;
    float knockbackDuration, restoreKnockbackDuration;

    private Canvas ui;
    public GameObject damageUI;
    public string damage;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rgbd = gameObject.GetComponent<Rigidbody2D>();

        knockbackDuration = 1f;
        restoreKnockbackDuration = knockbackDuration;

        ui = GetComponentInChildren<Canvas>();
        ui.worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    private void FixedUpdate()
    {
        if (isHurt)
        {
            knockbackDuration -= Time.fixedDeltaTime;
            if(knockbackDuration <= 0)
            {
                isHurt = false;
                knockbackDuration = restoreKnockbackDuration;
            }
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wood" && !player.isHurt)
        {
            int voice = Random.Range(1, 5);
            FindObjectOfType<AudioManager>().Play("charahurt" + voice);

            player.playerAnim.SetTrigger("hurt");
            player.HUD.value -= 1f;
            player.fill.color = player.HUDgradient.Evaluate(player.HUD.normalizedValue);

            player.hurtLight.SetActive(true);

            damage = "-1";
            Instantiate(damageUI, transform.position, transform.rotation, ui.transform);
        }
        if (collision.gameObject.tag == "RockMissile" && !player.isHurt)
        {
            int voice = Random.Range(1, 5);
            FindObjectOfType<AudioManager>().Play("charahurt" + voice);

            rgbd.AddForce(new Vector2(2f, 1f).normalized * -500f);
            player.playerAnim.SetTrigger("hurt");
            player.HUD.value -= 3f;
            player.fill.color = player.HUDgradient.Evaluate(player.HUD.normalizedValue);
            player.isHurt = true;
            isHurt = true;

            player.hurtLight.SetActive(true);

            damage = "-3";
            Instantiate(damageUI, transform.position, transform.rotation, ui.transform);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cutter" && !player.isHurt)
        {
            player.HUD.value -= 0.06f;
            player.fill.color = player.HUDgradient.Evaluate(player.HUD.normalizedValue);

            player.hurtLight.SetActive(true);

            damage = "-0.06";
            Instantiate(damageUI, transform.position, transform.rotation, ui.transform);
        }
    }
}
