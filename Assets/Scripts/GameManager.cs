using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private EnemyManager enemyManager;

    private HeroManager heroManager;

    private VFXManager vFXManager;

    private AudioManager audioManager;

    private SaveManager saveManager;
    [HideInInspector]
    public float UIHealthBarMaxWidth;

    [HideInInspector]
    [SerializeField] private GameObject bossBar;
    [HideInInspector]
    [SerializeField] private float maxBossBarWidthUI;
    [HideInInspector]
    [SerializeField] private float currentBossBarWidthUI;

    [HideInInspector]
    [SerializeField] private float maxBossBarPoints;
    [HideInInspector]
    [SerializeField] public float currentBossBarPoints;

    [Tooltip ("Ilosc punktow do boss bara co zabojstwo")] 
    [SerializeField] private float bossBarPointPerKill;

    [HideInInspector]
    [SerializeField] private float bossBarPointsToWidthUI;

    private GameObject startBossButton;

    private float timerFighter;
    private float timerRanger;
    private int rangerHitCounter;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        heroManager = GetComponent<HeroManager>();
        vFXManager = GetComponent<VFXManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        saveManager = GetComponent<SaveManager>();

        UIHealthBarMaxWidth = GameObject.Find("HealthDisplay").GetComponent<RectTransform>().sizeDelta.x;
        bossBar = GameObject.Find("BossBarDisplay");

        startBossButton = GameObject.Find("StartBoss");
        
        maxBossBarWidthUI = bossBar.GetComponent<RectTransform>().sizeDelta.x;
        //saveManager.SaveGame();
        
    }

    void Start()
    {
        saveManager.LoadGame();

        bossBarPointsToWidthUI = maxBossBarWidthUI / maxBossBarPoints;

        BossBarUIUpdate();

    }



    void FixedUpdate()
    {
        
    }

     void Update()
    {
        tapDamageCall();

        perTimeDamageCallFighter();

        perTimeDamageCallRanger();

    }

    private void perTimeDamageCallFighter()
    {
        timerFighter += Time.deltaTime;
        if (timerFighter > heroManager.TimeFighter && !vFXManager.isCorutineRunning)
        {
            vFXManager.OnScreenAdditionalHeroAttackEffect();
            enemyManager.currentEnemy.GetComponent<Enemy>().DamageEnemy(heroManager.DamagePerTimeFighter);
            timerFighter = 0;
        }
    }

    private void perTimeDamageCallRanger()
    {
        
        timerRanger += Time.deltaTime;
        if (timerRanger > heroManager.TimeRanger && !vFXManager.isCorutineRunning)
        {
            vFXManager.OnScreenAdditionalHeroAttackEffect();            
            
            if (rangerHitCounter > heroManager.Xattacks) 
            {
                enemyManager.currentEnemy.GetComponent<Enemy>().DamageEnemy(heroManager.DamagePerTimeRanger + heroManager.ExtraDamagePerXattacks);
                rangerHitCounter = 0;
            }
            else
            {
                enemyManager.currentEnemy.GetComponent<Enemy>().DamageEnemy(heroManager.DamagePerTimeRanger);
            }

            rangerHitCounter++;
            timerRanger = 0;
        }
    }

    private void tapDamageCall()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            enemyManager.currentEnemy.GetComponent<Enemy>().DamageEnemy(heroManager.TapDamage);
            vFXManager.OnScreenMainHeroAttackEffect();
            audioManager.PlaySFX("Wind");
        }
    }




    public void BossBarPointsUpdate()
    {
        if (!enemyManager.currentEnemy.name.Contains("Boss"))
        {
            currentBossBarPoints += bossBarPointPerKill;
        }
    }

    public void BossBarUIUpdate()
    {
        if (currentBossBarPoints >= maxBossBarPoints)
        {
            startBossButton.SetActive(true);
            currentBossBarPoints = maxBossBarPoints;
        }
        else
        {
            startBossButton.SetActive(false);
        }

        currentBossBarWidthUI = currentBossBarPoints * bossBarPointsToWidthUI;
        bossBar.GetComponent<RectTransform>().sizeDelta = new Vector2(currentBossBarWidthUI, bossBar.GetComponent<RectTransform>().sizeDelta.y);
    }






}
