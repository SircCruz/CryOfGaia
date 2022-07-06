using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashAttack : MonoBehaviour
{
    Rigidbody2D rgbd;
    Transform trans;
    float speed = 450f;

    public float delay = 1f;
    private void Start()
    {
        rgbd = GetComponentInParent<Rigidbody2D>();
        trans = rgbd.GetComponent<Transform>();
        StartCoroutine(Dash());
    }
    IEnumerator Dash()
    {
        yield return new WaitForSeconds(delay);
        rgbd.AddForce(new Vector2(speed * Time.fixedDeltaTime * trans.localScale.x, transform.position.y), ForceMode2D.Impulse);
    }
    private void OnDestroy()
    {
        rgbd.velocity = Vector3.zero;
    }
}
