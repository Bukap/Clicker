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





}
