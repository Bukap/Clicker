using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [Tooltip("Obrazenia od tego konkretnego bohatera. Dodawane do tapowania")]
    public float Damage;
    [Tooltip("Efekt wizualny tapowania")]
    public GameObject AttackVFX;
    [Tooltip("Nazwa wyswietlana w grze")]
    public string Name;
    [Tooltip("Obrazek wyswietlany w UI")]
    public Sprite UIimage;
    public string Description;


    void Start()
    {
        Name = gameObject.name;

        
    }

    public string AddDescription()
    {
        Description = Name + "\n" + "Damage: " + Damage.ToString() + "\n";
        return Description;
    }
}
