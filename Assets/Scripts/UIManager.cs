using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject mainCamera;

    private GameObject shopPanel;
    private GameObject combatPanel;

    [SerializeField] private float transitionSpeed;

    private enum panels { combat, shop };
    private panels currentPanel;

    void Awake()
    {
        mainCamera = GameObject.Find("MainCamera");
        shopPanel = GameObject.Find("BackgroundShop");
        combatPanel = GameObject.Find("BackgroundCombat");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentPanel)
        {
            case panels.combat:
                goToCombat();
                break;

            case panels.shop:
                goToShop(); 
                break;
        }
    }


    public void switchToShop()
    {
        currentPanel = panels.shop;
    } 

    public void switchToCombat()
    {
        currentPanel = panels.combat;
    }

    private void goToShop()
    {
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, shopPanel.transform.position, transitionSpeed);
    }

    private void goToCombat()
    {
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, combatPanel.transform.position, transitionSpeed);
    }
}
