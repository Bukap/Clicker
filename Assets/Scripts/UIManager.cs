using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private EconomyManager economyManager;

    private GameObject mainCamera;

    private GameObject shopPanel;
    private GameObject combatPanel;

    [SerializeField] private float transitionSpeed;

    private enum panels { combat, shop };
    private panels currentPanel;

    private TextMeshProUGUI currency1Text;
    private TextMeshProUGUI currency2Text;
    private TextMeshProUGUI currency3Text;
    private TextMeshProUGUI currency4Text;

    void Awake()
    {
        economyManager = GetComponent<EconomyManager>();

        mainCamera = GameObject.Find("MainCamera");
        shopPanel = GameObject.Find("ShopPanel");
        combatPanel = GameObject.Find("CombatPanel");
       
        currency1Text = GameObject.Find("Currency1").transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>();
        currency2Text = GameObject.Find("Currency2").transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>();
        currency3Text = GameObject.Find("Currency3").transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>();
        currency4Text = GameObject.Find("Currency4").transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>();

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

    public void CurrencyUpdate()
    {
        currency1Text.text = economyManager.Currency1.ToString();
        currency2Text.text = economyManager.Currency2.ToString();
        currency3Text.text = economyManager.Currency3.ToString();
        currency4Text.text = economyManager.Currency4.ToString();

    }
}
