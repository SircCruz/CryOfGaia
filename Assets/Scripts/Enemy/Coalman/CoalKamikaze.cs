using UnityEngine;

public class CoalKamikaze : MonoBehaviour
{
    Coalman coalman;
    Player player;
    private void Start()
    {
        coalman = GetComponentInParent<Coalman>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Siga"))
        {
            coalman.hitPoints -= 0.1f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.Hurt();
        }
    }
}
    
