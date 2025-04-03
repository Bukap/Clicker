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
        shopPanel = GameObject.Find("ShopPanel");
        combatPanel = GameObject.Find("CombatPanel");

        print("combat panel "+ combatPanel.transform.position);
    }

    void Start()
    {
        
    }


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

    #region ScreenSwitch
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
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, new Vector3(shopPanel.transform.position.x, 0, mainCamera.transform.position.z), transitionSpeed);
    }

    private void goToCombat()
    {
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, new Vector3(combatPanel.transform.position.x, 0, mainCamera.transform.position.z), transitionSpeed);
    }

    #endregion

    #region ItemSmelt
    public void markItemVisual_ItemSmelter(GameObject itemSmelterButton)
    {
        swithStatus(itemSmelterButton.transform.GetChild(0).gameObject);
        swithStatus(itemSmelterButton.transform.GetChild(1).gameObject);
    }
    private void swithStatus(GameObject checkmark)
    {
        switch (checkmark.activeInHierarchy) 
        {
            case true:
                checkmark.SetActive(false); 
                break;

            case false:
                checkmark.SetActive(true); 
                break;
        }
    }
    #endregion
}
