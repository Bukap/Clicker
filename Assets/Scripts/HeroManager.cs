using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    [Tooltip("Sumaryczne obrazenia co tapniecie (poki co to sa tylko obrazenia od bohatera, ale kiedys bedziemy tu obliczac tez dodatkowe perki z broni itp.)")]
     public float TapDamage;
    [Tooltip("Sumaryczne obrazenia co sekunde (poki co to sa tylko obrazenia od bohaterów, ale kiedys bedziemy tu obliczac tez dodatkowe perki z broni itp.)")]
     public float PerSecondDamage;

    [HideInInspector]
    public MainHero MainHeroGameObject;
    public GameObject AdditionalHeroSlot;
    public List<GameObject> AdditionalHeroList = new List<GameObject>();


     void Awake()
    {
        AdditionalHeroSlot = GameObject.Find("AdditionalHeroesSlot");

        #region assigningHeroesVariables

        MainHeroGameObject = GameObject.Find("MainHeroSlot").transform.GetChild(0).GetComponent<MainHero>();

        foreach (Transform additionalHero in AdditionalHeroSlot.transform)
        {
            AdditionalHeroList.Add(additionalHero.gameObject);
        }
        #endregion
    }

    void Start()
    {
        updateDamage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateDamage()
    {
        TapDamage = MainHeroGameObject.Damage;

        float a = 0;
        foreach(GameObject additionalHero in AdditionalHeroList)
        {
            a += additionalHero.GetComponent<AdditionalHero>().Damage;
        }
        PerSecondDamage = a;
    }
}
