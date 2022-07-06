using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public GameObject trash;
    
    C2Mission2 mission;
    void Start()
    {
        mission = GameObject.Find("Task Monitor").GetComponent<C2Mission2>();
    }
    private void Update()
    {
        if (mission.isHoldingTrash)
        {
            gameObject.layer = LayerMask.NameToLayer("Task Item");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Task Object");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Rigidbody2D rgbd = gameObject.GetComponent<Rigidbody2D>();
            rgbd.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (mission.isHoldingTrash)
            {
                mission.AddCount();
                mission.isHoldingTrash = false;
            }
        }
        if (collision.gameObject.CompareTag("EnemyAttackCollider"))
        {
            if (mission.progress.value > 0)
            {
                mission.DeductCount();
                Instantiate(trash, transform.position, transform.rotation);
            }
        }
    }
}
