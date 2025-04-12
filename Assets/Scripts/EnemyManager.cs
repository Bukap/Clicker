using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameManager gameManager;
    private EconomyManager economyManager;
    private SaveManager saveManager;

    [Tooltip ("Lista przeciwnikow do wylowowanie dla graczy")]
    [SerializeField] public List<GameObject> enemies;
    [Tooltip("Lista bossow do wylowowanie dla graczy")]
    [SerializeField] public List<GameObject> bosses;

    [HideInInspector]
    public GameObject currentEnemySlot;
    [HideInInspector]
    public GameObject currentEnemy;

    private GameObject UIHealthDisplay;


     void Awake()
    {
        gameManager = GetComponent<GameManager>();
        economyManager = GetComponent<EconomyManager>();
        saveManager = GetComponent<SaveManager>();

        currentEnemySlot = GameObject.Find("CurrentEnemySlot");

        UIHealthDisplay = GameObject.Find("HealthDisplay");
    }

    void Start()
    {
        createEnemy();
    }



    void Update()
    {
        
    }





    public void healthToUIBar_deathCheck()
    {
        UIHealthDisplay.GetComponent<RectTransform>().sizeDelta = new Vector2(gameManager.UIHealthBarMaxWidth * (currentEnemy.GetComponent<Enemy>().CurrentHealthPoints / currentEnemy.GetComponent<Enemy>().MaxHealthPoints), UIHealthDisplay.GetComponent<RectTransform>().sizeDelta.y);
        deathCheck();
    }
    private void deathCheck()
    {
        if (currentEnemy.GetComponent<Enemy>().CurrentHealthPoints <= 0)
        {
            gameManager.BossBarPointsUpdate();
            gameManager.BossBarUIUpdate();
            Destroy(currentEnemy.gameObject);
            createEnemy();
            economyManager.AddCurrency();
            saveManager.SaveGame();
        }
    }
    private void createEnemy()
    {
        currentEnemy = Instantiate(enemies[Random.Range(0, enemies.Count)], currentEnemySlot.transform);
        
    }

    public void CreateBoss()
    {
        Destroy(currentEnemy); gameManager.BossBarPointsUpdate();
        currentEnemy = Instantiate(bosses[Random.Range(0, bosses.Count)], currentEnemySlot.transform);

        gameManager.currentBossBarPoints = 0;
        gameManager.BossBarUIUpdate();
    }
}
