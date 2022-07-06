using UnityEngine;

public class Melee : MonoBehaviour
{
    public Animator meleeAnim;
    public BoxCollider2D meleeCollider;

    public float damage = 1.5f;
    float meleeDuration = 0.61f;
    private void Start()
    {
        damage = UpgradeCheck.MeleeDamage();

        int voice = Random.Range(1, 5);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("voice" + voice);

        int sound = Random.Range(1, 3);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("melee" + sound);
    }
    private void FixedUpdate()
    {
        meleeDuration -= Time.fixedDeltaTime;
        if(meleeDuration <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            int sound = Random.Range(1, 4);
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("meleehit" + sound);

            meleeAnim.SetTrigger("hit");
            meleeCollider.enabled = false;
            GameObject.Find("AudioManager").GetComponent<AudioManager>().Play("hit");
        }
    }
}
