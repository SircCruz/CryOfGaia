
using UnityEngine;

public class ChainsawSound : MonoBehaviour
{
    SpriteRenderer render;
    EnemyAttack enemyAttack;
    float visibleUpdateDur, restoreVisibleUpdateDur;

    public AudioClip walking, death;
    public AudioClip[] attacks;
    AudioSource source;

    bool isPlayed, isDeathPlayed;
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        render = gameObject.GetComponentInParent<SpriteRenderer>();
        enemyAttack = render.GetComponentInChildren<EnemyAttack>();
        visibleUpdateDur = 1f;
        restoreVisibleUpdateDur = visibleUpdateDur;

        source.volume = PlayerPrefs.GetFloat("Sound Volume", 0.1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        visibleUpdateDur -= Time.fixedDeltaTime;
        if (visibleUpdateDur <= 0)
        {
            if (render.isVisible && !isPlayed && !isDeathPlayed)
            {
                source.PlayOneShot(walking);
            }
            visibleUpdateDur = restoreVisibleUpdateDur;
        }
        if (enemyAttack != null)
        {
            if (enemyAttack.startAttack && !isPlayed)
            {
                int voice = Random.Range(1, 4);
                for (int i = 0; i < attacks.Length; i++)
                {
                    if (i == voice)
                    {
                        source.PlayOneShot(attacks[i]);
                        break;
                    }
                }
                isPlayed = true;
            }
            if (!enemyAttack.startAttack)
            {
                isPlayed = false;
            }
        }
    }
    public void Dead()
    {
        if(!isDeathPlayed)
        {
            source.PlayOneShot(death);
            isDeathPlayed = true;
        }
    }
}
