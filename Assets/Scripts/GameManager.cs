using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private EnemyManager enemyManager;

    private GameObject currentEnemy;

    [HideInInspector]
    public float UIHealthBarMaxWidth;


    private void Awake()
    {
        UIHealthBarMaxWidth = GameObject.Find("HealthBar").GetComponent<RectTransform>().sizeDelta.x;
        currentEnemy = GameObject.Find("CurrentEnemy");
    }

    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
    }



    void FixedUpdate()
    {
        if(currentEnemy.transform.childCount == 0)
        {
            CreateEnemy();
        }
    }

    private void CreateEnemy()
    {
        Instantiate(enemyManager.enemies[Random.Range(0, enemyManager.enemies.Count)], currentEnemy.transform);
    }



}
