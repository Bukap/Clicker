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
    [SerializeField] private float MaxUIHealthDisplay;



    void Start()
    {
        gameManager = GameObject.Find("MainCamera").GetComponent<GameManager>();

        currentEnemySlot = GameObject.Find("CurrentEnemySlot");

        UIHealthDisplay = GameObject.Find("HealthBar");

    }



    void Update()
    {
        
    }



    public void CreateEnemy()
    {
        GameObject e = Instantiate(enemies[Random.Range(0, enemies.Count)], currentEnemySlot.transform);
        currentEnemy = e;
    }

    public void healthToUIBar()
    {
        UIHealthDisplay.GetComponent<RectTransform>().sizeDelta = new Vector2(gameManager.UIHealthBarMaxWidth * (currentEnemy.GetComponent<Enemy>().CurrentHealthPoints / currentEnemy.GetComponent<Enemy>().MaxHealthPoints), UIHealthDisplay.GetComponent<RectTransform>().sizeDelta.y);
    }
}
