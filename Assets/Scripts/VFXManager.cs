using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class VFXManager : MonoBehaviour
{
    private HeroManager heroManager;
    private EnemyManager enemyManager;

    [Header("Main Hero")]

    [SerializeField] private GameObject tapVFX;
    private RectTransform safeArea;
    //private List<GameObject> vfxTapSpawnList;   //List of created tap vfx from tapping

    [Tooltip("Najwieksza ilosc obiektow od efektow wizulanych ktora moze byc na wygenerowana")]
    //[SerializeField] private int vfxObjextsCount;
    [Space]

    [Header("Additional Hero")]

    [SerializeField] private List<GameObject> additionalHeroVFXList; // List of VFX gameobjects
    [Tooltip("Zasieg w jakim pojawi sie vfx dodatkowych bohaterow w zgledem pozycji przeciwnika (podana liczba brana jest osobno na + i -)")]
    [SerializeField] private Vector2 vfxPositionMaxNoise;
    [Tooltip("Drobna roznica w czasie pomiedzy kolejnymi efektami")]
    [SerializeField] private float vfxSpawnTimeMaxNoise;
    //[SerializeField] private List<GameObject> vfxDPSpawnList;    //List of created DPS vfx from additional heroes attacks
    public bool isCorutineRunning = false;


    void Awake()
    {
        heroManager = GetComponent<HeroManager>();
        enemyManager = GetComponent<EnemyManager>();  
        safeArea = GameObject.Find("SafeArea").GetComponent<RectTransform>();
        tapVFX = heroManager.MainHero.AttackVFX;
        
    }

    void Start()
    {
        #region VFX List Instantiation
        
        #endregion
    }

    void Update()
    {
        
    }

    public void OnScreenMainHeroAttackEffect()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            safeArea,
            Input.GetTouch(0).position,
            Camera.main, 
            out Vector2 localPoint
        );

        // Creating vfx on screen,
        // seting the position to the there the player clicked on screen,
        // adding vfx object to list,
        // deleting oldest if more than 10 are in the list

        GameObject vfxObject = Instantiate(tapVFX, safeArea);
        vfxObject.transform.localPosition =  localPoint;
        //vfxTapSpawnList.Add(vfxObject);
        /*
        if(vfxTapSpawnList.Count > vfxObjextsCount)
        {
            Destroy(vfxTapSpawnList[0]);
            vfxTapSpawnList.RemoveAt(0);
        }
        */
    }

    public void OnScreenAdditionalHeroAttackEffect()
    {
        StartCoroutine(OnScreenAdditionalHeroAttackEffectCorutine());
    }

    IEnumerator OnScreenAdditionalHeroAttackEffectCorutine()
    {
        isCorutineRunning = true;

        foreach(GameObject vfx in additionalHeroVFXList)
        {
            GameObject vfxObject = Instantiate(vfx);
            vfxObject.transform.position = new Vector2(enemyManager.currentEnemySlot.transform.position.x + Random.Range(-vfxPositionMaxNoise.x, vfxPositionMaxNoise.x), enemyManager.currentEnemySlot.transform.position.y + Random.Range(-vfxPositionMaxNoise.y, vfxPositionMaxNoise.y));
            //vfxDPSpawnList.Add(vfxObject);
            yield return new WaitForSeconds(Random.Range(0,vfxSpawnTimeMaxNoise));
        }
        /*
        if (vfxDPSpawnList.Count > heroManager.AdditionalHeroSlot.transform.childCount)
        {
            for (int i = 0; i> heroManager.AdditionalHeroSlot.transform.childCount; i++)
            {
                Destroy(vfxTapSpawnList[i]);
                vfxDPSpawnList.RemoveAt(i);
            }
        }
        */
        isCorutineRunning = false;
    }
}
