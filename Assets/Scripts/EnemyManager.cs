using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] public List<GameObject> enemies;
    [SerializeField] public List<GameObject> bosses;

    public GameObject currentEnemySlot;
    public GameObject currentEnemy;

    [SerializeField] private GameObject UIHealthDisplay;


    void Start()
    {
        gameManager = GameObject.Find("MainCamera").GetComponent<GameManager>();

        currentEnemySlot = GameObject.Find("CurrentEnemySlot");

        UIHealthDisplay = GameObject.Find("HealthBar");

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
