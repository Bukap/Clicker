using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AdditionalHero : MonoBehaviour
{

    [Header("Fighter")]
    [Tooltip("Obrazenia co czas od tego konkretnego bohatera")]
    public float DamagePerTimeFighter;
    public float TimeFighter;

    [Header("Ranger")]
    [Tooltip("Dodatkowe Obrazenia co X atak od tego konkretnego bohatera")]
    public float DamagePerTimeRanger;
    public float ExtraDamagePerXattacks;
    public int Xattacks;
    public float TimeRanger;

    [Header("Support")]
    [Header("Dla pe³nej sprawnoœci nalezy dodac + lub x przed liczba aby odpowiedno dodac lub przemnozyc wartosc")]
    [Tooltip("Booster obrazen dla main hero")]
    public string MainHeroFlatDamageBoost;
    [Tooltip("Booster obrazen dla side hero")]
    public string SideHeroFlatDamageBoost;

    [Header("Shaman")]
    [Header("Dla pe³nej sprawnoœci nalezy dodac + lub x przed liczba aby odpowiedno dodac lub przemnozyc wartosc")]
    [Tooltip("Booster lootu A")]
    public string LootA;
    [Tooltip("Booster lootu B")]
    public string LootB;
    [Tooltip("Booster lootu C")]
    public string LootC;
    [Tooltip("Booster lootu D")]
    public string LootD;

    [Tooltip("Efekt wizualny dla ataków")]
    public GameObject AttackVFX;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}


[CustomEditor(typeof(AdditionalHero))]
public class AdditionalHeroEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AdditionalHero additionalHero = (AdditionalHero)target;
        GameObject gameObject = ((AdditionalHero)target).gameObject;

        EditorGUILayout.LabelField("Tag:", gameObject.tag);


        switch (gameObject.tag)
        {
            case ("Fighter"):
                SerializedProperty DamagePerTimeFighter = serializedObject.FindProperty("DamagePerTimeFighter");
                EditorGUILayout.PropertyField(DamagePerTimeFighter);
                SerializedProperty TimeFighter = serializedObject.FindProperty("TimeFighter");
                EditorGUILayout.PropertyField(TimeFighter);
                break;
            case ("Ranger"):
                SerializedProperty DamagePerTimeRanger = serializedObject.FindProperty("DamagePerTimeRanger");
                EditorGUILayout.PropertyField(DamagePerTimeRanger);
                SerializedProperty ExtraDamagePerXattacks = serializedObject.FindProperty("ExtraDamagePerXattacks");
                EditorGUILayout.PropertyField(ExtraDamagePerXattacks);
                SerializedProperty Xattacks = serializedObject.FindProperty("Xattacks");
                EditorGUILayout.PropertyField(Xattacks);
                SerializedProperty TimeRanger = serializedObject.FindProperty("TimeRanger");
                EditorGUILayout.PropertyField(TimeRanger);
                break;
            case ("Support"):
                SerializedProperty MainHeroFlatDamageBoost = serializedObject.FindProperty("MainHeroFlatDamageBoost");
                EditorGUILayout.PropertyField(MainHeroFlatDamageBoost);
                SerializedProperty SideHeroFlatDamageBoost = serializedObject.FindProperty("SideHeroFlatDamageBoost");
                EditorGUILayout.PropertyField(SideHeroFlatDamageBoost);
                break;
            case ("Shaman"):
                SerializedProperty LootA = serializedObject.FindProperty("LootA");
                EditorGUILayout.PropertyField(LootA);
                SerializedProperty LootB = serializedObject.FindProperty("LootB");
                EditorGUILayout.PropertyField(LootB);
                SerializedProperty LootC = serializedObject.FindProperty("LootC");
                EditorGUILayout.PropertyField(LootC);
                SerializedProperty LootD = serializedObject.FindProperty("LootD");
                EditorGUILayout.PropertyField(LootD);
                break;

        }

        serializedObject.ApplyModifiedProperties();


        if (GUI.changed)
        {
            EditorUtility.SetDirty(additionalHero);
        }
    }
}