using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] public float MaxHealthPoints;
    [SerializeField] public float CurrentHealthPoints;

    [SerializeField] private GameObject UIHealthDisplay;

    [SerializeField] private float MaxUIHealthDisplay;

    void Start()
    {
        gameManager = GameObject.Find("MainCamera").GetComponent<GameManager>();    

        CurrentHealthPoints = MaxHealthPoints;
        UIHealthDisplay = GameObject.Find("HealthBar");

        healthToUIBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyTap(float damage)
    {
        CurrentHealthPoints -= damage;
        healthToUIBar();

        deathCheck();
    }

    private void healthToUIBar()
    {
        UIHealthDisplay.GetComponent<RectTransform>().sizeDelta = new Vector2(gameManager.UIHealthBarMaxWidth * (CurrentHealthPoints/MaxHealthPoints), UIHealthDisplay.GetComponent<RectTransform>().sizeDelta.y);
    }

    private void deathCheck()
    {
        if (CurrentHealthPoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
