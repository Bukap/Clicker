using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameManager gameManager;
    public EnemyManager enemyManager;

    [SerializeField] public float MaxHealthPoints;
    [SerializeField] public float CurrentHealthPoints;

    void Start()
    {
        gameManager = GameObject.Find("MainCamera").GetComponent<GameManager>();
        enemyManager = GameObject.Find("MainCamera").GetComponent<EnemyManager>(); 
        
        CurrentHealthPoints = MaxHealthPoints;

        enemyManager.healthToUIBar_deathCheck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageEnemy(float damage)
    {
        CurrentHealthPoints -= damage;
        enemyManager.healthToUIBar_deathCheck();
    }

    

    
}
