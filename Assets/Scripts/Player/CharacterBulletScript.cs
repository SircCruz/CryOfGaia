using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CharacterBulletScript : MonoBehaviour
{
    public Animator bulletAnim;
    CharacterController2D player;
    public CircleCollider2D bulletCollider;
    public SpriteRenderer bulletColor;

    public float bulletSpeed = 20f;
    public float damage = 2;

    Vector3 moveSpeed;

    bool isHit = false;
    float hitAnimDuration = 0.61f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
        damage = UpgradeCheck.RangedDamage();
        bulletColor.color = UpgradeCheck.RangedVisualUpgrade();

        int sounds = Random.Range(1, 4);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("fireball" + sounds);

        int genVoice = Random.Range(1, 100);
        if(genVoice >= 60)
        {
            int voice = Random.Range(1, 4);
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("rangedvoice" + voice);
        }

        if (!player.m_FacingRight)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            moveSpeed = Vector3.left;
        }
        else
        {
            moveSpeed = Vector3.right;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHit)
        {
            transform.Translate(moveSpeed * bulletSpeed * Time.fixedDeltaTime);
        }
        if (isHit)
        {
            transform.Translate(moveSpeed * bulletSpeed * Time.fixedDeltaTime * 0);
            hitAnimDuration -= Time.fixedDeltaTime;
            if (hitAnimDuration <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bulletAnim.SetTrigger("hit");
        bulletCollider.enabled = false;
        isHit = true;
        if(collision.gameObject.tag == "Game World")
        {
            Destroy(gameObject);
        }
    }
}
