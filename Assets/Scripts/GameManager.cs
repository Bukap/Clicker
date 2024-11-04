using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private EnemyManager enemyManager;

    private HeroManager heroManager; 

    private VFXManager vFXManager;

    [HideInInspector]
    public float UIHealthBarMaxWidth;

    [SerializeField] private GameObject bossBar;
    [SerializeField] private float maxBossBarWidthUI;
    [SerializeField] private float currentBossBarWidthUI;
    [SerializeField] private float maxBossBarPoints;
    [SerializeField] public float currentBossBarPoints;
    [SerializeField] private float bossBarPointPerKill;
    [SerializeField] private float bossBarPointsToWidthUI;
    private GameObject startBossButton;

    private float oneSecond = 1;
    private float timer;

    [SerializeField] GameObject heroVFX;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        heroManager = GetComponent<HeroManager>();
        vFXManager = GetComponent<VFXManager>();

        UIHealthBarMaxWidth = GameObject.Find("HealthBar").GetComponent<RectTransform>().sizeDelta.x;
        bossBar = GameObject.Find("BossBar");

        startBossButton = GameObject.Find("StartBoss");
        
        maxBossBarWidthUI = bossBar.GetComponent<RectTransform>().sizeDelta.x;

    }

    void Start()
    {
        
        bossBarPointsToWidthUI = maxBossBarWidthUI / maxBossBarPoints;

        BossBarUIUpdate();
        heroVFX = heroManager.MainHeroGameObject.AttackVFX;

    }



    void FixedUpdate()
    {
        
    }

    private void Update()
    {
        #region tapDamageCall
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            enemyManager.currentEnemy.GetComponent<Enemy>().DamageEnemy(heroManager.TapDamage);
            vFXManager.OnScreenEffect(heroVFX);
        }
        #endregion

        #region damagePerSecondCall
        timer += Time.deltaTime;
        if (timer > oneSecond)
        {
            enemyManager.currentEnemy.GetComponent<Enemy>().DamageEnemy(heroManager.PerSecondDamage);
            timer = 0;
        }
        #endregion
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
