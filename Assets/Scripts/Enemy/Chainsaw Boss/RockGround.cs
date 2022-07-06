using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class RockGround : MonoBehaviour
{
    float rockHP = 15;
    public CharacterBulletScript bullet;
    public Melee melee;
    public Skill1 skill1;
    public GameObject heart;
    public Animator rockAnim;
    CharacterAttacks skill2;
    SceneLoader app;
    WorldPhysics camera;

    Light2D rockLight;

    void OnApplicationQuit()
    {
        app.isQuitting = true;
    }
    void OnDestroy()
    {
        if (!app.isQuitting)
        {
            int genHeart = Random.Range(1, 10);
            if (genHeart <= 2)
            {
                Instantiate(heart, new Vector2(transform.position.x, transform.position.y + 1.5f), transform.rotation);
            }
            rockAnim.SetBool("Exit", true);
        }
    }
    void Start()
    {
        skill2 = GameObject.Find("Character").GetComponent<CharacterAttacks>();
        app = GameObject.FindGameObjectWithTag("Game Master").GetComponent<SceneLoader>();
        rockLight = gameObject.GetComponentInChildren<Light2D>();
        camera = app.GetComponent<WorldPhysics>();

        FindObjectOfType<AudioManager>().Play("rockground");

        camera.CameraShake(0.5f, 2);
    }
    // Update is called once per frame
    void Update()
    {
        if(rockHP <= 10)
        {
            rockAnim.SetBool("Break1", true);
        }
        if(rockHP <= 5)
        {
            rockAnim.SetBool("Break2", true);
        }
        if(rockHP <= 0)
        {
            rockAnim.SetBool("Destroy", true);
            Destroy(gameObject, 0.7f);
        }
    }
    private void FixedUpdate()
    {
        if (rockLight.enabled == true)
        {
            rockLight.intensity -= Time.fixedDeltaTime * 2;
            if (rockLight.intensity <= 0)
            {
                rockLight.enabled = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            rockHP -= bullet.damage;
        }
        if (collision.gameObject.tag == "Melee")
        {
            rockHP -= melee.damage;
        }
        if (collision.gameObject.tag == "Skill1")
        {
            rockHP -= skill1.damage;
        }
        if (collision.gameObject.tag == "Skill2")
        {
            rockHP -= skill2.skill2AttackDamage;
        }
    }
}
