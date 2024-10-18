using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    [SerializeField] public float TapDamage;
    [SerializeField] public float PerSecondDamage;

    private MainHero mainHero;
    private List<GameObject> additionalHeroList = new List<GameObject>();

    void Start()
    {
        #region assigningHeroesVariables
        mainHero = GameObject.Find("MainHeroSlot").transform.GetChild(0).GetComponent<MainHero>();

        print(GameObject.Find("AdditionalHeroesSlot").transform);

        foreach (Transform child in GameObject.Find("AdditionalHeroesSlot").transform)
        {
            additionalHeroList.Add(child.gameObject);
        }
        #endregion

        updateDamage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateDamage()
    {
        TapDamage = mainHero.Damage;

        float a = 0;
        foreach(GameObject additionalHero in additionalHeroList)
        {
            a += additionalHero.GetComponent<AdditionalHero>().Damage;
        }
        PerSecondDamage = a;
    }
}
