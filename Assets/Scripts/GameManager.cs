using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private EnemyManager enemyManager;

    private HeroManager heroManager;

    [SerializeField] private VFXManager vFXManager;

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

    private float timer;
    private float oneSecond = 1;

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

    }



    void FixedUpdate()
    {
        
    }

     void Update()
    {
        #region tapDamageCall
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            enemyManager.currentEnemy.GetComponent<Enemy>().DamageEnemy(heroManager.TapDamage);
            vFXManager.OnScreenMainHeroAttackEffect();
        }
        #endregion

        #region perSecondDamageCall
        timer += Time.deltaTime;
        if (timer > oneSecond && !vFXManager.isCorutineRunning)
        {
            vFXManager.OnScreenAdditionalHeroAttackEffect(); 
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
