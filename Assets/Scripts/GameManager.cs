using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private EnemyManager enemyManager;

    private HeroManager heroManager; 

    [HideInInspector]
    public float UIHealthBarMaxWidth;

    private float oneSecond = 1;
    private float timer;

    private void Awake()
    {
        UIHealthBarMaxWidth = GameObject.Find("HealthBar").GetComponent<RectTransform>().sizeDelta.x;
    }

    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        heroManager = GetComponent<HeroManager>();

        enemyManager.CreateEnemy();
    }



    void FixedUpdate()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            enemyManager.currentEnemy.GetComponent<Enemy>().DamageEnemy(heroManager.TapDamage);
        }

        timer += Time.deltaTime;
        if (timer > oneSecond)
        {
            enemyManager.currentEnemy.GetComponent<Enemy>().DamageEnemy(heroManager.PerSecondDamage);
            timer = 0;
        }
    }





}
