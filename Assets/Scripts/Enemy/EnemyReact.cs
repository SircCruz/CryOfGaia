using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReact : MonoBehaviour
{
    private Transform character;
    private Vector2 getCharacterPos;
    private float characterPos;

    public Rigidbody2D enemy;
    private float skill1knockBackPower = 100f;

    private float stunDuration = 5f;
    private float restoreStunDuration;
    private bool isStunned = false;

    private void Start()
    {
        restoreStunDuration = stunDuration;
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        getCharacterPos = new Vector2(character.transform.position.x, character.transform.position.y);
        characterPos = getCharacterPos.x;
    }
    private void FixedUpdate()
    {
        if (isStunned)
        {
            stunDuration -= Time.fixedDeltaTime;
            if(stunDuration <= 0)
            {
                stunDuration = restoreStunDuration;
                isStunned = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Skill1")
        {
            if (characterPos < transform.position.x)
            {
                enemy.AddForce(new Vector2(transform.position.x + skill1knockBackPower, transform.position.y) * Time.deltaTime, ForceMode2D.Impulse);
            }
            else if (characterPos > transform.position.x)
            {
                enemy.AddForce(new Vector2(transform.position.x - skill1knockBackPower, transform.position.y) * Time.deltaTime, ForceMode2D.Impulse);
            }
        }
        if(collision.gameObject.tag == "Skill2")
        {
            if (!isStunned)
            {
                isStunned = true;
            }
        }
    }
}
