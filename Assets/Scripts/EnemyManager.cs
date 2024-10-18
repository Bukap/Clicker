using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] public List<GameObject> enemies;

    public GameObject currentEnemySlot;
    public GameObject currentEnemy;

    [SerializeField] private GameObject UIHealthDisplay;


    void Start()
    {
        gameManager = GameObject.Find("MainCamera").GetComponent<GameManager>();

        currentEnemySlot = GameObject.Find("CurrentEnemySlot");

        UIHealthDisplay = GameObject.Find("HealthBar");

        CreateEnemy();
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
            Destroy(currentEnemy.gameObject);
            CreateEnemy();
        }
    }
    private void CreateEnemy()
    {
        currentEnemy = Instantiate(enemies[Random.Range(0, enemies.Count)], currentEnemySlot.transform);
        
    }
}
