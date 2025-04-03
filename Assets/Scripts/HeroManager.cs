using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class HeroManager : MonoBehaviour
{

    private EconomyManager economyManager;

    [Tooltip("Sumaryczne obrazenia co tapniecie (poki co to sa tylko obrazenia od bohatera, ale kiedys bedziemy tu obliczac tez dodatkowe perki z broni itp.)")]
     public float TapDamage;

    [Header("Fighter")]
    public float DamagePerTimeFighter;
    public float TimeFighter;

    [Header("Ranger")]
    public float DamagePerTimeRanger;
    public float ExtraDamagePerXattacks;
    public int Xattacks;
    public float TimeRanger;

    [Header("Support")]
    public string MainHeroFlatDamageBoost;
    public string SideHeroFlatDamageBoost;

    [Header("Shaman")]
    public string LootA;
    public string LootB;
    public string LootC;
    public string LootD;

    private GameObject mainHeroSlot;
    private GameObject fighterSlot;
    private GameObject rangerSlot;
    private GameObject supportSlot;
    private GameObject shamanSlot;

    [HideInInspector] public MainHero MainHero;
    [HideInInspector] public Weapon MainHeroWeapon;
    [HideInInspector] public AdditionalHero FighterAdditionalHero;
    [HideInInspector] public AdditionalHero RangerAdditionalHero;
    [HideInInspector] public AdditionalHero SupportAdditionalHero;
    [HideInInspector] public AdditionalHero ShamanAdditionalHero;

    [SerializeField] private GameObject weaponSocket;

     void Awake()
     {
        economyManager = GetComponent<EconomyManager>();

        #region assigningHeroesSlots
        mainHeroSlot = GameObject.Find("MainHeroSlot");
        fighterSlot = GameObject.Find("Fighter");
        rangerSlot = GameObject.Find("Ranger");
        supportSlot = GameObject.Find("Support");
        shamanSlot = GameObject.Find("Shaman");     
        #endregion
        UpdateStats();
     }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void assignHeroesVariables()
    {
        MainHero = economyManager.EquipedMainHero.GetComponent<MainHero>();
        MainHeroWeapon = economyManager.EquipedWeapon.GetComponent<Weapon>();
        FighterAdditionalHero = economyManager.EquipedAdditionalHeroesFighter.GetComponent<AdditionalHero>();
        RangerAdditionalHero = economyManager.EquipedAdditionalHeroesRanger.GetComponent<AdditionalHero>();
        SupportAdditionalHero = economyManager.EquipedAdditionalHeroesSupport.GetComponent<AdditionalHero>();
        ShamanAdditionalHero = economyManager.EquipedAdditionalHeroesShaman.GetComponent<AdditionalHero>();
    }

    private void spawnHeroes()
    {
        if (mainHeroSlot.transform.childCount>0)
        {
            foreach (Transform child in mainHeroSlot.transform)
            {
                Destroy(child.gameObject);
            }
        }
        GameObject a = Instantiate(economyManager.EquipedMainHero);
        a.transform.SetParent(mainHeroSlot.transform,false);
        weaponSocket = a.GetComponentsInChildren<Transform>(true).FirstOrDefault(t => t.name == "WeaponSocket")?.gameObject;

        if (weaponSocket.transform.childCount > 0)
        {
            foreach (Transform child in GameObject.Find("WeaponSocket").transform.transform)
            {
                Destroy(child.gameObject);
            }
        }
        GameObject b = Instantiate(economyManager.EquipedWeapon);
        b.transform.SetParent(weaponSocket.transform, false);
        print(b.transform.position + b.name);

        if (fighterSlot.transform.childCount > 0)
        {
            foreach (Transform child in fighterSlot.transform)
            {
                Destroy(child.gameObject);
            }
        }
        GameObject c = Instantiate(economyManager.EquipedAdditionalHeroesFighter);
        c.transform.SetParent(fighterSlot.transform, false);

        if (rangerSlot.transform.childCount > 0)
        {
            foreach (Transform child in rangerSlot.transform)
            {
                Destroy(child.gameObject);
            }
        }
        GameObject d = Instantiate(economyManager.EquipedAdditionalHeroesRanger);
        d.transform.SetParent(rangerSlot.transform, false);

        if (shamanSlot.transform.childCount > 0)
        {
            foreach (Transform child in shamanSlot.transform)
            {
                Destroy(child.gameObject);
            }
        }
        GameObject e = Instantiate(economyManager.EquipedAdditionalHeroesShaman);
        e.transform.SetParent(shamanSlot.transform, false);

        if (supportSlot.transform.childCount > 0)
        {
            foreach (Transform child in supportSlot.transform)
            {
                Destroy(child.gameObject);
            }
        }
        GameObject f = Instantiate(economyManager.EquipedAdditionalHeroesSupport);
        f.transform.SetParent(supportSlot.transform, false);
    }

    public void UpdateStats()
    {
        assignHeroesVariables();
        spawnHeroes();

        #region Support
        MainHeroFlatDamageBoost = SupportAdditionalHero.MainHeroFlatDamageBoost;
        SideHeroFlatDamageBoost = SupportAdditionalHero.SideHeroFlatDamageBoost;
        #endregion
        #region Hero
        TapDamage = MainHero.Damage + MainHeroWeapon.Damage;
        BoostHandler(MainHeroFlatDamageBoost, TapDamage);
        #endregion
        #region Fighter
        DamagePerTimeFighter = FighterAdditionalHero.DamagePerTimeFighter;
        BoostHandler(SideHeroFlatDamageBoost, DamagePerTimeFighter);
        TimeFighter = FighterAdditionalHero.TimeFighter;
        #endregion
        #region Ranger
        DamagePerTimeRanger = RangerAdditionalHero.DamagePerTimeRanger;
        BoostHandler(SideHeroFlatDamageBoost, DamagePerTimeRanger);
        ExtraDamagePerXattacks = RangerAdditionalHero.ExtraDamagePerXattacks;
        BoostHandler(SideHeroFlatDamageBoost, ExtraDamagePerXattacks);
        Xattacks = RangerAdditionalHero.Xattacks;
        TimeRanger = RangerAdditionalHero.TimeRanger;
        #endregion
        #region Shaman
        LootA = SupportAdditionalHero.LootA;
        LootB = SupportAdditionalHero.LootB;
        LootC = SupportAdditionalHero.LootC;
        LootD = SupportAdditionalHero.LootD;
        #endregion
}



    public void BoostHandler(string boostDetails, float boostedVariable)
    {
        switch (boostDetails[0])
        {
            case ('x'):
                boostedVariable = boostedVariable * float.Parse(boostDetails.Substring(1, boostDetails.Length - 1));
                break;
            case ('+'):
                boostedVariable = boostedVariable + float.Parse(boostDetails.Substring(1, boostDetails.Length - 1));
                break;
            default:
                Debug.LogError(this.name + "Ma problem z otrzymana wartoscia, sprawdz czy ma x lub + przed liczba. Ew, wartosc jest pusta");
                break;
        }
    }


}



    


