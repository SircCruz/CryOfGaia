using UnityEngine;

public class EnemyAttackGenerator : MonoBehaviour
{
    //Attach this script if the AI does have more than 1 target
    public bool enableRetargeting;

    Transform player;
    public Transform enemy;

    private float sightRange = 5f;
    private bool isGenerateAtk = false;
    private int attacks = 1;

    [SerializeField] private GameObject[] target;

    private void OnEnable()
    {
        isGenerateAtk = false;
    }
    private void OnDisable()
    {
        for (int i = 0; i < target.Length; i++)
        {
            target[i].SetActive(false);
        }
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        attacks = Random.Range(0, target.Length);
        Attack();
    }
    private void Update()
    {
        if (player.transform.position.x <= enemy.transform.position.x + sightRange && player.transform.position.x >= enemy.transform.position.x - sightRange)
        {
            if (enableRetargeting)
            {
                for (int i = 1; i < target.Length; i++)
                {
                    target[i].SetActive(false);
                }
                attacks = 0;
                isGenerateAtk = true;
            }
        }
        else
        {
            if (!isGenerateAtk)
            {
                for(int i = 0; i < target.Length; i++)
                {
                    target[i].SetActive(false);
                }
                attacks = Random.Range(0, target.Length);
                isGenerateAtk = true;
            }
        }
        Attack();
    }
    public void Attack()
    {
        for (int i = 0; i < target.Length; i++)
        {
            if (attacks == i)
            {
                target[i].SetActive(true);
                break;
            }
        }
    }
}
