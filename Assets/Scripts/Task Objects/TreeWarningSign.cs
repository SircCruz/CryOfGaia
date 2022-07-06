using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeWarningSign : MonoBehaviour
{
    public Animator warningLeft, bottomLeft, bottomRight, warningRight;
    public Transform character;
    private float charaXPos;

    //1=left tree 2=center tree 3=right tree 
    public int position;

    // Update is called once per frame
    void Update()
    {
        charaXPos = character.position.x;

        if(position == 1)
        {
            if (charaXPos <= -32)
            {
                warningLeft.SetBool("warning", false);
            }
        }
        if(position == 2)
        {
            if(charaXPos >= -10 && charaXPos <= 10)
            {
                bottomLeft.SetBool("warning", false);
                bottomRight.SetBool("warning", false);
            }
        }
        if(position == 3)
        {
            if(charaXPos >= 29)
            {
                warningRight.SetBool("warning", false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttackCollider"))
        {
            if (position == 1)
            {
                if (charaXPos >= -32)
                {
                    warningLeft.SetBool("warning", true);
                }
            }
            if(position == 2)
            {
                if(charaXPos <= -10)
                {
                    bottomRight.SetBool("warning", true);
                }
                if(charaXPos >= 10)
                {
                    bottomLeft.SetBool("warning", true);
                }
            }
            if(position == 3)
            {
                if(charaXPos <= 29)
                {
                    warningRight.SetBool("warning", true);
                }
            }
        }
    }
}
