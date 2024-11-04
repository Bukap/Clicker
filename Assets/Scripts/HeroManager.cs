using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    [SerializeField] public float TapDamage;
    [SerializeField] public float PerSecondDamage;

    public MainHero MainHeroGameObject;
    private List<GameObject> additionalHeroList = new List<GameObject>();

     void Awake()
    {
        #region assigningHeroesVariables

        MainHeroGameObject = GameObject.Find("MainHeroSlot").transform.GetChild(0).GetComponent<MainHero>();

        foreach (Transform child in GameObject.Find("AdditionalHeroesSlot").transform)
        {
            additionalHeroList.Add(child.gameObject);
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
        foreach(GameObject additionalHero in additionalHeroList)
        {
            a += additionalHero.GetComponent<AdditionalHero>().Damage;
        }
        PerSecondDamage = a;
    }
}
