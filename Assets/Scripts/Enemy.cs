using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [SerializeField] public float MaxHealthPoints;
    [SerializeField] public float CurrentHealthPoints;

    [SerializeField] private GameObject UIHealthDisplay;

    [SerializeField] private float MaxUIHealthDisplay;

    void Start()
    {
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
    }

    private void healthToUIBar()
    {
        UIHealthDisplay.GetComponent<RectTransform>().sizeDelta = new Vector2(1000f*(CurrentHealthPoints/MaxHealthPoints), UIHealthDisplay.GetComponent<RectTransform>().sizeDelta.y);
    }

}
