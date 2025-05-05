using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private EconomyManager economyManager;

    private string savePath;


    void Awake()
    {
        savePath = Application.persistentDataPath + "/save.json";
        economyManager = GetComponent<EconomyManager>();
    }

    private SaveData ReadGameData()
    {
        SaveData saveData = new SaveData();

        saveData.Currency1 = economyManager.Currency1;
        saveData.Currency2 = economyManager.Currency2;
        saveData.Currency3 = economyManager.Currency3;
        saveData.Currency4 = economyManager.Currency4;

        saveData.EquipedWeaponName = economyManager.EquipedWeapon.name;
        saveData.EquipedMainHeroName = economyManager.EquipedMainHero.name;
        saveData.EquipedAdditionalHeroesFighterName = economyManager.EquipedAdditionalHeroesFighter.name;
        saveData.EquipedAdditionalHeroesRangerName = economyManager.EquipedAdditionalHeroesRanger.name;
        saveData.EquipedAdditionalHeroesSupportName = economyManager.EquipedAdditionalHeroesSupport.name;
        saveData.EquipedAdditionalHeroesShamanName = economyManager.EquipedAdditionalHeroesShaman.name;

        return saveData;
    }

    public void SaveGame()
    {
        SaveData data = ReadGameData();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

    }

    private void ApplyGameData(SaveData data)
    {
        economyManager.Currency1 = data.Currency1;
        economyManager.Currency2 = data.Currency2;
        economyManager.Currency3 = data.Currency3;
        economyManager.Currency4 = data.Currency4;

        economyManager.EquipedWeapon = economyManager.OwnedWeapons.Find(obj => obj.name == data.EquipedWeaponName);
        economyManager.EquipedMainHero = economyManager.OwnedMainHero.Find(obj => obj.name == data.EquipedMainHeroName);
        economyManager.EquipedAdditionalHeroesFighter = economyManager.OwnedAdditionalHeroesFighters.Find(obj => obj.name == data.EquipedAdditionalHeroesFighterName);
        economyManager.EquipedAdditionalHeroesRanger = economyManager.OwnedAdditionalHeroesRangers.Find(obj => obj.name == data.EquipedAdditionalHeroesRangerName);
        economyManager.EquipedAdditionalHeroesSupport= economyManager.OwnedAdditionalHeroesSupports.Find(obj => obj.name == data.EquipedAdditionalHeroesSupportName);
        economyManager.EquipedAdditionalHeroesShaman= economyManager.OwnedAdditionalHeroesShamans.Find(obj => obj.name == data.EquipedAdditionalHeroesShamanName);
    }

    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);         
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            ApplyGameData(data);
        }
        else
        {
            Debug.LogWarning("no save file found");
        }
    }

    

    
}

[System.Serializable]
public class SaveData
{
    public uint Currency1;
    public uint Currency2;
    public uint Currency3;
    public uint Currency4;

    public string EquipedWeaponName;
    public string EquipedMainHeroName;
    public string EquipedAdditionalHeroesFighterName;
    public string EquipedAdditionalHeroesRangerName;
    public string EquipedAdditionalHeroesSupportName;
    public string EquipedAdditionalHeroesShamanName;

}
