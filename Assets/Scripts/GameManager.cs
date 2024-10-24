using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private EnemyManager enemyManager;

    private HeroManager heroManager; 

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

    private void Awake()
    {
        UIHealthBarMaxWidth = GameObject.Find("HealthBar").GetComponent<RectTransform>().sizeDelta.x;
        bossBar = GameObject.Find("BossBar");

        startBossButton = GameObject.Find("StartBoss");
    }

    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        heroManager = GetComponent<HeroManager>();

        maxBossBarWidthUI = bossBar.GetComponent<RectTransform>().sizeDelta.x;
        bossBarPointsToWidthUI = maxBossBarWidthUI / maxBossBarPoints;

        BossBarUIUpdate();
    }



    void FixedUpdate()
    {
        
    }

    private void Update()
    {
        #region tapDamageCall
        if (Input.GetMouseButtonDown(0))
        {
            enemyManager.currentEnemy.GetComponent<Enemy>().DamageEnemy(heroManager.TapDamage);
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
