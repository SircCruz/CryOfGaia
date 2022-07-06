using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections;

public class ChainsawBoss : MonoBehaviour
{
    public Animator launchAnim, topFireAnim, bottomFireAnim, cutterAnim, chainsawBossAnim;
    public GameObject chainsawBack, cutterG, topTurret, bottomTurret, mortar, lightG;

    public GameObject missileLight, woodLightTop, woodLightBottom;
    public Light2D lightScript, woodLightTopScript, woodLightBottomScript;
    bool startLight, startWoodLightTop, startWoodLightBottom, atTop;
    float lightDur, restoreLightDur, woodLightTopDur, restoreWoodLightTopDur, woodLightBottomDur, restoreWoodLightBottomDur;

    public AchievementCounter killCount;

    public CharacterBulletScript bullet;
    public Melee melee;
    public Skill1 skill1;
    public CharacterAttacks skill2;
    public GameObject cutterExtended;
    public Slider chainsawHUD;
    public Gradient chainsawHUDGradient;
    public Image fill;
    WorldPhysics cam;

    //Audio
    AudioSource source;
    public AudioClip[] sound;
    float playTime, restorePlayTime;
    bool isPlayed;

    int genAttack;
    Transform character;
    public GameObject rockMissile;
    public GameObject wood;
    public GameObject cutter;

    Vector2 offSet;

    bool isAttacking = false;
    bool isHalfHealth = false;
    bool isPlayedDeath = false;

    int oneAttack = 1, basicAttack = 1;

    float attackCooldown;
    float restoreCooldown;

    float cutterDur, cutterDur2;
    float restoreCutterDur, restoreCutterDur2;
    bool isCutting = false;
    bool isCutting2 = false;

    bool isFire, isDie;

    float preparationDur, restorePreparationDur;

    //Animation vars
    float launchAnimDelay, restoreLaunchAnimDelay;
    bool startLaunchAnim = false;

    float fireAnimDelay, restoreFireAnimDelay;
    bool startFireAnim = false;

    //variables for rockSuccession attack
    float spawnRockets, restoreSpawnRockets;
    bool startFiring = false;
    int rockets, rocketsCounter;

    //varibles for beastmode attack
    float spawnCooldown;
    float restoreSpawnCooldown;
    bool startShooting = false;
    int bullets, bulletsCounter;
    int subCounter, subCounter2;
    bool isAtTop;

    SceneLoader app;
    public Skill2 skill22;
    void Start()
    {
        fill.color = chainsawHUDGradient.Evaluate(1f);

        attackCooldown = 2f;
        restoreCooldown = attackCooldown;

        cutterDur = 1f;
        restoreCutterDur = cutterDur;
        cutterDur2 = 1f;
        restoreCutterDur2 = cutterDur2;

        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        app = GameObject.FindGameObjectWithTag("Game Master").GetComponent<SceneLoader>();
        cam = app.GetComponent<WorldPhysics>();
        source = gameObject.GetComponent<AudioSource>();

        rockets = 2;

        spawnRockets = 1f;
        restoreSpawnRockets = spawnRockets;

        spawnCooldown = 0.4f;
        restoreSpawnCooldown = spawnCooldown;

        preparationDur = 2f;
        restorePreparationDur = preparationDur;

        launchAnimDelay = 0.8f;
        restoreLaunchAnimDelay = launchAnimDelay;

        fireAnimDelay = 0.8f;
        restoreFireAnimDelay = fireAnimDelay;

        lightDur = 0.5f;
        restoreLightDur = lightDur;
        missileLight.SetActive(false);

        woodLightTopDur = 0.2f;
        restoreWoodLightTopDur = woodLightTopDur;
        woodLightTop.SetActive(false);

        woodLightBottomDur = 0.2f;
        restoreWoodLightBottomDur = woodLightBottomDur;
        woodLightBottom.SetActive(false);

        playTime = 10f;
        restorePlayTime = playTime;

        skill22.tutorialMode = false;
    }
    void Update()
    {
        if(chainsawHUD.value >= 650 && !isHalfHealth)
        {
            BasicAttack();
        }
        if(chainsawHUD.value <= 649)
        {
            isHalfHealth = true;
            GenAttack();
        }
        if(chainsawHUD.value <= 0)
        {
            chainsawBack.SetActive(false);
            topTurret.SetActive(false);
            bottomTurret.SetActive(false);
            cutterG.SetActive(false);
            mortar.SetActive(false);
            lightG.SetActive(false);
            chainsawBossAnim.SetBool("Death", true);
            startShooting = false;
            startFiring = false;
            killCount.lookAtChainsawBoss();
            lightScript.enabled = false;
            woodLightTopScript.enabled = false;
            woodLightBottomScript.enabled = false;

            if (!isPlayedDeath)
            {
                FindObjectOfType<AudioManager>().Play("chainsawbossdeath");
                StartCoroutine(DeathSound());
                isPlayedDeath = true;
            }

            Destroy(gameObject, 5.13f);
        }
        if (!isAttacking)
        {
            BasicAttack();
            isAttacking = true;
        }
    }
    void FixedUpdate()
    {
        //play audio
        if (!isPlayed)
        {
            playTime -= Time.fixedDeltaTime;
            if(playTime <= 0)
            {
                int sound = Random.Range(0, 3);
                source.PlayOneShot(this.sound[sound], PlayerPrefs.GetFloat("Sound Volume", 0.1f));
                playTime = restorePlayTime;
            }
        }
        //attack cooldown
        if (isAttacking && !isHalfHealth)
        {
            attackCooldown -= Time.fixedDeltaTime;
            if (attackCooldown <= 0)
            {
                basicAttack = 1;
                attackCooldown = restoreCooldown;
                isAttacking = false;
            }
        }
        //light from missile
        if (startLight)
        {
            missileLight.SetActive(true);
            lightScript.intensity = lightDur * 5;
            lightDur -= Time.fixedDeltaTime;
            if(lightDur <= 0)
            {
                missileLight.SetActive(false);
                startLight = false;
                lightDur = restoreLightDur;
            }
        }
        //light from wood
        if (startWoodLightTop)
        {
            woodLightTop.SetActive(true);
            woodLightTopScript.intensity = woodLightTopDur * 10;
            woodLightTopDur -= Time.fixedDeltaTime;
            if(woodLightTopDur <= 0)
            {
                woodLightTop.SetActive(false);
                startWoodLightTop = false;
                woodLightTopDur = restoreWoodLightTopDur;
            }
        }
        if (startWoodLightBottom)
        {
            woodLightBottom.SetActive(true);
            woodLightBottomScript.intensity = woodLightBottomDur * 10;
            woodLightBottomDur -= Time.fixedDeltaTime;
            if (woodLightBottomDur <= 0)
            {
                woodLightBottom.SetActive(false);
                startWoodLightBottom = false;
                woodLightBottomDur = restoreWoodLightBottomDur;
            }
        }
        //if cutter is active
        if (isCutting)
        {
            cutterDur -= Time.fixedDeltaTime;
            if(cutterDur <= 0.5f)
            {
                cutter.SetActive(true);
            }
            if(cutterDur <= 0)
            {
                cutterDur = restoreCutterDur;
                isCutting = false;
                cutter.SetActive(false);
                cutterAnim.SetBool("Flex", false);
                isAttacking = false;
            }
        }
        if (isCutting2)
        {
            cutterDur2 -= Time.fixedDeltaTime;
            if (cutterDur2 <= 0.5f)
            {
                cutter.SetActive(true);
            }
            if (cutterDur2 <= 0)
            {
                cutterDur2 = restoreCutterDur2;
                cutter.SetActive(false);
                cutterAnim.SetBool("Flex", false);
                oneAttack = 1;
                GenAttack();
                isCutting2 = false;
            }
        }
        //for rocketSuccession attack
        if (startFiring)
        {
            if (rocketsCounter <= rockets)
            {
                if(chainsawHUD.value > 400)
                {
                    launchAnim.SetBool("Launch2", true);
                }
                else
                {
                    launchAnim.SetBool("Launch3", true);
                }
                spawnRockets -= Time.fixedDeltaTime;
                if (spawnRockets <= 0)
                {
                    Instantiate(rockMissile, new Vector2(transform.position.x + -1.36f, transform.position.y + 5f), transform.rotation);
                    startLight = true;
                    rocketsCounter++;
                    spawnRockets = restoreSpawnRockets;
                }
            }
            else
            {
                launchAnim.SetBool("Launch2", false);
                launchAnim.SetBool("Launch3", false);
                preparationDur -= Time.fixedDeltaTime;
                if(preparationDur <= 0)
                {
                    oneAttack = 1;
                    rocketsCounter = 0;
                    preparationDur = restorePreparationDur;
                    startFiring = false;
                    GenAttack();
                }
            }
        }
        //for beastmode attack
        if (startShooting)
        {
            if (bulletsCounter <= bullets)
            {
                spawnCooldown -= Time.fixedDeltaTime;
                if (spawnCooldown <= 0)
                {
                    Vector2 offSetTop = new Vector2(transform.position.x + 2f, transform.position.y + 2.3f);
                    Vector2 offSetBottom = new Vector2(transform.position.x + 2f, transform.position.y - 1.3f);
                    if (isAtTop)
                    {
                        bottomFireAnim.SetBool("Fire2Fast", true);
                        startWoodLightBottom = true;
                        Instantiate(wood, offSetBottom, transform.rotation);
                        if (subCounter == 3)
                        {
                            topFireAnim.SetBool("Fire", true);
                            Instantiate(wood, offSetTop, transform.rotation);
                            startWoodLightTop = true;
                            startFireAnim = true;
                            subCounter = 0;
                        }
                        else
                        {
                            topFireAnim.SetBool("Fire", false);
                            subCounter++;
                        }
                    }
                    else
                    {
                        topFireAnim.SetBool("FireFast", true);
                        Instantiate(wood, offSetTop, transform.rotation);
                        startWoodLightTop = true;
                        if (subCounter2 == 3)
                        {
                            startLaunchAnim = true;
                            launchAnim.SetBool("Launch", true);
                            subCounter2 = 0;
                            
                        }
                        else
                        {
                            launchAnim.SetBool("Launch", false);
                            subCounter2++;
                        }
                    }
                    bulletsCounter += 1;
                    spawnCooldown = restoreSpawnCooldown;
                }
            }
            else
            {
                topFireAnim.SetBool("FireFast", false);
                bottomFireAnim.SetBool("Fire2Fast", false);
                preparationDur -= Time.fixedDeltaTime;
                if (preparationDur <= 0)
                {
                    oneAttack = 1;
                    bulletsCounter = 0;
                    preparationDur = restorePreparationDur;
                    startShooting = false;
                    GenAttack();
                }
            }
        }
        //Animations
        // Launch Anim
        if (startLaunchAnim)
        {
            launchAnimDelay -= Time.fixedDeltaTime;
            if (launchAnimDelay <= 0)
            {
                launchAnim.SetBool("Launch", false);
                Vector2 offSet = new Vector2(transform.position.x - 1.36f, transform.position.y + 5f);
                cam.CameraShake(0.2f, 0.8f);
                Instantiate(rockMissile, offSet, transform.rotation);
                startLight = true;
                launchAnimDelay = restoreLaunchAnimDelay;
                startLaunchAnim = false;
            }
        }
        if (startFireAnim)
        {
            fireAnimDelay -= Time.fixedDeltaTime;
            if(fireAnimDelay <= 0)
            {
                topFireAnim.SetBool("Fire", false);
                bottomFireAnim.SetBool("Fire2", false);
                Instantiate(wood, offSet, transform.rotation);
                if (atTop)
                {
                    startWoodLightTop = true;
                }
                else
                {
                    startWoodLightBottom = true;
                }

                isAttacking = false;
                fireAnimDelay = restoreFireAnimDelay;
                startFireAnim = false;
            }
        }
    }
    void Attack(float index)
    {
        //launch rockMissile
        if(index == 1)
        {
            launchAnim.SetBool("Launch", true);
            startLaunchAnim = true;
        }
        //launch wood
        if(index == 2)
        {
            if (character.transform.position.y >= -1.305396)
            {
                offSet = new Vector2(transform.position.x + 2f, transform.position.y + 2.3f);
                topFireAnim.SetBool("Fire", true);
                atTop = true;
            }
            else
            {
                offSet = new Vector2(transform.position.x + 2f, transform.position.y - 1.3f);
                bottomFireAnim.SetBool("Fire2", true);
                atTop = false;
            }
            startFireAnim = true;
        }
        //flex cutter
        if(index == 3)
        {
            if(character.transform.position.x >= -9)
            {
                if (!isCutting)
                {
                    FindObjectOfType<AudioManager>().Play("cutter");
                    isCutting = true;
                    cutterAnim.SetBool("Flex", true);
                }
            }
            else
            {
                isAttacking = false;
            }
        }
        //rock succession
        if(index == 4)
        {
            if (!isFire)
            {
                startFiring = true;
                if(chainsawHUD.value <= 600)
                {
                    isFire = true;
                }
            }
            else
            {
                isFire = false;
                Attack(Random.Range(5, 7));
            }
        }
        //beast mode
        if (index == 5)
        {
            if (isAtTop)
            {
                isAtTop = false;
            }
            else
            {
                isAtTop = true;
            }
            bullets = Random.Range(0, 41);
            startShooting = true;
        }
        //just die attack
        if(index == 6)
        {
            if (!isDie)
            {
                if (character.transform.position.x >= -9)
                {
                    if (!isCutting2)
                    {
                        isCutting2 = true;
                        cutterAnim.SetBool("Flex", true);
                        FindObjectOfType<AudioManager>().Play("cutter");
                    }
                }
                else
                {
                    isCutting2 = false;
                    oneAttack = 1;
                    GenAttack();
                }
                isDie = true;
            }
            else
            {
                isCutting2 = false;
                oneAttack = 1;
                GenAttack();
                isDie = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            chainsawHUD.value -= bullet.damage;
        }
        if (collision.gameObject.tag == "Melee")
        {
            chainsawHUD.value -= melee.damage;
        }
        if (collision.gameObject.tag == "Skill1")
        {
            chainsawHUD.value -= skill1.damage;
        }
        if (collision.gameObject.tag == "Skill2")
        {
            chainsawHUD.value -= skill2.skill2AttackDamage;
        }
        fill.color = chainsawHUDGradient.Evaluate(chainsawHUD.normalizedValue);
    }

    private void BasicAttack()
    {
        if (basicAttack == 1)
        {
            if (chainsawHUD.value >= 800)
            {
                genAttack = Random.Range(1, 4);
                restoreCooldown = 1f;
            }
            else if (chainsawHUD.value >= 700)
            {
                genAttack = Random.Range(1, 4);
                rockets = 2;
            }
            else if (chainsawHUD.value >= 650)
            {
                genAttack = Random.Range(1, 4);
                rockets = 4;
                restoreCooldown = 0.5f;
            }
            basicAttack = 0;
            Attack(genAttack);
        }
    }
    private void GenAttack()
    {
        if(oneAttack == 1)
        {
            if (chainsawHUD.value >= 400)
            {
                genAttack = Random.Range(4, 7);
                restoreSpawnRockets = 1f;
                restoreCooldown = 3f;
            }
            else if (chainsawHUD.value <= 399)
            {
                restorePlayTime = 6f;
                genAttack = Random.Range(4, 7);
                restoreSpawnRockets = 0.5f;
                rockets = 7;
                restorePreparationDur = 1.5f;
            }
            oneAttack = 0;
            Attack(genAttack);
        }
    }
    IEnumerator DeathSound()
    {
        for(int i = 0; i < 5; i++)
        {
            source.PlayOneShot(sound[Random.Range(3,6)], PlayerPrefs.GetFloat("Sound Volume", 0.1f));
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    private void OnDestroy()
    {
        if (!app.isQuitting)
        {
            PlayerPrefs.SetInt("C1BossMission", 1);

            killCount.AddCount();
            chainsawBossAnim.SetBool("Exit", true);
            killCount.startCounting = true;
        }
    }
}
