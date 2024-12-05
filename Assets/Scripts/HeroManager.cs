using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HeroManager : MonoBehaviour
{

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
    [HideInInspector] public AdditionalHero FighterAdditionalHero;
    [HideInInspector] public AdditionalHero RangerAdditionalHero;
    [HideInInspector] public AdditionalHero SupportAdditionalHero;
    [HideInInspector] public AdditionalHero ShamanAdditionalHero;



     void Awake()
    {
        #region assigningHeroesSlots
         mainHeroSlot = GameObject.Find("MainHeroSlot");
         fighterSlot = GameObject.Find("Fighter");
         rangerSlot = GameObject.Find("Ranger");
         supportSlot = GameObject.Find("Support");
         shamanSlot = GameObject.Find("Shaman");
        #endregion

        #region assigningHeroesVariables
        MainHero = mainHeroSlot.transform.GetChild(0).GetComponent<MainHero>();
        FighterAdditionalHero = fighterSlot.transform.GetChild(0).GetComponent<AdditionalHero>();
        RangerAdditionalHero = rangerSlot.transform.GetChild(0).GetComponent<AdditionalHero>();
        SupportAdditionalHero = supportSlot.transform.GetChild(0).GetComponent<AdditionalHero>();
        ShamanAdditionalHero = shamanSlot.transform.GetChild(0).GetComponent<AdditionalHero>();

        #endregion
    }

    void Start()
    {
        updateStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateStats()
    {
        #region Support
        MainHeroFlatDamageBoost = SupportAdditionalHero.MainHeroFlatDamageBoost;
        SideHeroFlatDamageBoost = SupportAdditionalHero.SideHeroFlatDamageBoost;
        #endregion
        #region Hero
        TapDamage = MainHero.Damage;
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



    


