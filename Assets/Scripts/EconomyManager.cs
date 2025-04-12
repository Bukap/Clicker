using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class EconomyManager : MonoBehaviour
{
    private HeroManager heroManager;
    private UIManager uiManager;

    [SerializeField] private GameObject equipButtonUI;
    [SerializeField] private GameObject swapPopup;
    [SerializeField] private GameObject confirmSwapingPopup;
    
    public List<GameObject> OwnedWeapons = new List<GameObject>();
    public List<GameObject> OwnedMainHero = new List<GameObject>();

    public List<GameObject> OwnedAdditionalHeroesFighters;
    public List<GameObject> OwnedAdditionalHeroesRangers;
    public List<GameObject> OwnedAdditionalHeroesSupports;
    public List<GameObject> OwnedAdditionalHeroesShamans;

    public uint Currency1;
    public uint Currency2;
    public uint Currency3;
    public uint Currency4;

    public GameObject EquipedWeapon;
    public GameObject EquipedMainHero;
    public GameObject EquipedAdditionalHeroesFighter;
    public GameObject EquipedAdditionalHeroesRanger;
    public GameObject EquipedAdditionalHeroesSupport;
    public GameObject EquipedAdditionalHeroesShaman;

    [SerializeField] private List<GameObject> equipment = new List<GameObject>();



    void Awake()
     {
        heroManager = GetComponent<HeroManager>();        
        uiManager = GetComponent<UIManager>();
        loadPrefabs();
    }

    void Start()
    {
        uiManager.CurrencyUpdate();
    }

    

    void Update()
    {

    }

    private void loadPrefabs()
    {
        GameObject[] spiritPrefabs = Resources.LoadAll<GameObject>("MainHeroPrefabs");
        OwnedMainHero.AddRange(spiritPrefabs);
        GameObject[] weaponPrefabs = Resources.LoadAll<GameObject>("WeaponPrefabs");
        OwnedWeapons.AddRange(weaponPrefabs);
        GameObject[] fighterPrefabs = Resources.LoadAll<GameObject>("AdditionalHeroFighterPrefabs");
        OwnedAdditionalHeroesFighters.AddRange(fighterPrefabs);
        GameObject[] rangerPrefabs = Resources.LoadAll<GameObject>("AdditionalHeroRangerPrefabs");
        OwnedAdditionalHeroesRangers.AddRange(rangerPrefabs);
        GameObject[] shamanPrefabs = Resources.LoadAll<GameObject>("AdditionalHeroShamanPrefabs");
        OwnedAdditionalHeroesShamans.AddRange(shamanPrefabs);
        GameObject[] supportPrefabs = Resources.LoadAll<GameObject>("AdditionalHeroSupportPrefabs");
        OwnedAdditionalHeroesSupports.AddRange(supportPrefabs);
        
    }

    public void openEquipment(string chosenEquipment)
    {
        foreach(Transform button in swapPopup.transform.Find("Masking/Grouping"))
        {
            Destroy(button.gameObject);
        }

        switch (chosenEquipment) {
            case "MainHero":
                equipment = OwnedMainHero;
                break;
            case "Weapon":
                equipment = OwnedWeapons;
                break;
            case "Fighter":
                equipment = OwnedAdditionalHeroesFighters;
                break;
            case "Ranger":
                equipment = OwnedAdditionalHeroesRangers;
                break;
            case "Support":
                equipment = OwnedAdditionalHeroesSupports;
                break;
            case "Shaman":
                equipment = OwnedAdditionalHeroesShamans;
                break;

        }

        for (int i = 0; i < equipment.Count; i++)
        {
            GameObject a = Instantiate(equipButtonUI);
            a.transform.SetParent(swapPopup.transform.Find("Masking/Grouping").transform, false);

            GameObject item = equipment[i]; // Get the item safely

            if (item.GetComponent<MainHero>() != null)
                a.transform.GetChild(0).GetComponent<Image>().sprite = item.GetComponent<MainHero>().UIimage;
            else if (item.GetComponent<Weapon>() != null)
                a.transform.GetChild(0).GetComponent<Image>().sprite = item.GetComponent<Weapon>().UIimage;
            else if (item.GetComponent<AdditionalHero>() != null)
                a.transform.GetChild(0).GetComponent<Image>().sprite = item.GetComponent<AdditionalHero>().UIimage;
            else
                print("ERROR");

            int buttonIndex = i;
            a.GetComponent<Button>().onClick.AddListener(() => ItemConfirmation(equipment[buttonIndex]));
        }
    }

    public void ItemConfirmation(GameObject item) 
    {
        confirmSwapingPopup.SetActive(true);
        swapPopup.SetActive(false);

        Image sprite = confirmSwapingPopup.transform.Find("SelectedHero/Image").GetComponent<Image>();
        TextMeshProUGUI description = confirmSwapingPopup.transform.Find("StatsDisplay").GetComponent<TextMeshProUGUI>();

        print(confirmSwapingPopup.transform.Find("SelectedHero/Image").name);
        print(confirmSwapingPopup.transform.Find("StatsDisplay").name);


        if (item.GetComponent<MainHero>() != null) 
        {
            sprite.sprite = item.GetComponent<MainHero>().UIimage;
            description.text = item.GetComponent<MainHero>().AddDescription();
            
        }
        else if (item.GetComponent<Weapon>() != null) 
        {
            sprite.sprite = item.GetComponent<Weapon>().UIimage;
            description.text = item.GetComponent<Weapon>().AddDescription();
        }
        else if (item.GetComponent<AdditionalHero>() != null) 
        {
            sprite.sprite = item.GetComponent<AdditionalHero>().UIimage;
            description.text = item.GetComponent<AdditionalHero>().AddDescription();
        }
        else
            print("ERROR");

        confirmSwapingPopup.transform.Find("Yes").GetComponent<Button>().onClick.AddListener( () => EquipItem(item));

    }

    private void EquipItem(GameObject item)
    {
        switch (item.tag)
        {
            case "MainHero":
                EquipedMainHero = item;
                break;
            case "Weapon":
                EquipedWeapon = item;
                break;
            case "Fighter":
                EquipedAdditionalHeroesFighter = item;
                break;
            case "Ranger":
                EquipedAdditionalHeroesRanger = item;
                break;
            case "Support":
                EquipedAdditionalHeroesSupport = item;
                break;
            case "Shaman":
                EquipedAdditionalHeroesShaman = item;
                break;
            default:
                print("error");
                break;
        }
        heroManager.UpdateStats();
    }

    public void LevelUpSideHero()
    {
        EquipedAdditionalHeroesFighter.GetComponent<AdditionalHero>().Level++;
        EquipedAdditionalHeroesRanger.GetComponent<AdditionalHero>().Level++;
        EquipedAdditionalHeroesSupport.GetComponent<AdditionalHero>().Level++;
        EquipedAdditionalHeroesShaman.GetComponent<AdditionalHero>().Level++;

        heroManager.UpdateStats();
    }
    public void LevelUpMainHero()
    {
        EquipedMainHero.GetComponent<MainHero>().Level++;

        heroManager.UpdateStats();
    }
    public void AddCurrency()
    {
        Currency1++;
        Currency2++;
        Currency3++;
        Currency4++;

        uiManager.CurrencyUpdate();
    }
}
