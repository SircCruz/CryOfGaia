using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    CharacterController2D player;
    GameObject maincamera;
    public SpriteRenderer skill1Color;

    public float bulletSpeed = 15f;
    public float damage;

    Vector3 moveSpeed;

    private void Start()
    {
        damage = UpgradeCheck.Skill1Damage();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
        maincamera = GameObject.FindGameObjectWithTag("MainCamera");
        skill1Color.color = UpgradeCheck.Skill1VisualUpgrade();

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
        int voice = Random.Range(1, 3);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("fireballskillvoice" + voice);

        int sound = Random.Range(1, 6);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("fireballskill" + sound);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(moveSpeed * bulletSpeed * Time.fixedDeltaTime);
        if(transform.position.x > maincamera.transform.position.x + 14f || transform.position.x < maincamera.transform.position.x - 14f)
        {
            Destroy(gameObject);
        }
    }
}
