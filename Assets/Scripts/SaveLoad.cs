using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour
{
    string saveFilePath;

    [SerializeField] float TimeInSeconds;

    public bool LoadSave;
    [SerializeField] bool AutoSave;
    [Space]
    public float GoatCoin;
    public float goatCoinPerSecond;
    public int[] shopItems;
    public int[] upgrades;
    public string[,] tiles;


    public ISaveLoad GameManager;

    void Awake()
    {
        saveFilePath = Application.persistentDataPath + "/GameSave.json";
    }

    private void FixedUpdate()
    {
        if (AutoSave)
            TimeInSeconds = TimeInSeconds + Time.deltaTime;
        if (TimeInSeconds >= 60)
        {
            SaveGame();
            TimeInSeconds = 0;
        }
    }

    class SavaData
    {
        public float GoatCoin = 0;
        public float goatCoinPerSecond = 0;
        public int[] shopItems = new int[1];
        public int[] upgrades = new int[1];
        public string[,] tiles = new string[1,1];
    }

    public void SaveGame()
    {
        if (!File.Exists(saveFilePath))
        {
            CreateSaveFile();
        }
        // call in GameManager to save
        SavaData savedata = new SavaData();
        savedata.GoatCoin = (float)Math.Round(GoatCoin);
        savedata.goatCoinPerSecond = goatCoinPerSecond;
        savedata.shopItems = shopItems;
        savedata.upgrades = upgrades;
        savedata.tiles = tiles;

        File.WriteAllText(saveFilePath, JsonUtility.ToJson(savedata));
        Debug.Log($"game saved!");
    }

    public void LoadGame()
    {
        // call at the start of Start function in gamemanager to load data
        if (!File.Exists(saveFilePath))
        {
            CreateSaveFile();
            return;
        }

        SavaData savedata = new SavaData();
        savedata = JsonUtility.FromJson<SavaData>(File.ReadAllText(saveFilePath));

        GoatCoin = savedata.GoatCoin;
        goatCoinPerSecond = savedata.goatCoinPerSecond;
        shopItems = savedata.shopItems;
        upgrades = savedata.upgrades;   
        tiles = savedata.tiles;
        
        Debug.Log($"saveData loaded!");
    }

    void CreateSaveFile()
    {
        Debug.Log("no save found. making new one");
        File.Create(saveFilePath);
    }
}
    
