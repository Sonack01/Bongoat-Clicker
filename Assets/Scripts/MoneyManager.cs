using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.Mathematics;
using JetBrains.Annotations;

public class MoneyManager : MonoBehaviour, ISaveLoad
{

    [SerializeField] private TextMeshProUGUI MoneyDisplay;
    [SerializeField] TextMeshProUGUI GCPSDisplay;
    [SerializeField] public float GoatCoin = 0f;

    [SerializeField] Button clicker;
    //float baseClickerValue = 1;
    [SerializeField] public float GoatCoinPerClick = 1;

    public GameObject[] ShopItems = new GameObject[10];

    GameObject shopItem;
    [SerializeField] GameObject ShopContent;

    [SerializeField] public ShopItemSO[] ShopItemsScriptableObjectsArray = new ShopItemSO[10];
    public int shopSpawnIndex = 0;

    public GameObject[] Upgrades = new GameObject[10];

    GameObject upgradesItem;
    [SerializeField] GameObject upgradesContent;

    [SerializeField] public UpgradesSO[] UpgradesScriptableObjectsArray = new UpgradesSO[10];
    public int upgradesSpawnIndex = 0;

    public float goatCoinPerSecond;

    [SerializeField] Button SaveBTN;
    [SerializeField] SaveLoad saveLoad;

    /*

    this script will be incharge of everything that has an impact on the goatcoin

    this will be what keeps track of adding money every click, how much GPS(goatcoin per second) well 
    be generated off ur bought shop item. it will also keep track of cost of thing and anytime u want 
    to make changes to money this script will be referenced

    */

    private void Awake() {

        shopItem = Resources.Load<GameObject>("Prefabs/shopItem");
        upgradesItem = Resources.Load<GameObject>("Prefabs/Upgrade");

        clicker.onClick.AddListener(TaskOnClick);


        SpawnShopItems();
        SpawnUpgrades();

        GCPSDisplay.text = "0";

        
    }

    private void Start() {
        saveLoad.LoadGame();
        
        if (saveLoad.LoadSave)
        {
            LoadData();
        }
    }

    void Update()
    {
        MoneyDisplay.text = (long)Math.Round(GoatCoin, 1) + " GC";
        
        GCPSDisplay.text = Math.Round(goatCoinPerSecond, 1).ToString() + " GC/s";

        if (Input.GetKeyDown(KeyCode.T))
        {
            AddMoney(100000);
        }

        if (Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.LeftShift))
        {
            AddMoney(100000);
        }
    }

    private void FixedUpdate() {
        CalculateGCPS();
        
        //GCPSDisplay.text = "0";
    }

    private void TaskOnClick()
    {
        GoatCoin += GoatCoinPerClick;
    }

    public void AddMoney(float amount)
    {
        GoatCoin += amount;
    }

    public void RemoveMoney(float amount)
    {
        GoatCoin -= amount;
    }

    void SpawnShopItems()
    {
        for (int i = 0; i < ShopItemsScriptableObjectsArray.Length - 1; i++)
        {
            ShopItems[i] = Instantiate(shopItem, ShopContent.transform.position, quaternion.identity, ShopContent.transform); 
            shopSpawnIndex++;
            if (ShopItemsScriptableObjectsArray[shopSpawnIndex] == null)
                break;
        }
    }

    void SpawnUpgrades()
    {
        for (int x = 0; x < UpgradesScriptableObjectsArray.Length - 1; x++)
        {
            Upgrades[x] = Instantiate(upgradesItem, upgradesContent.transform.position, quaternion.identity, upgradesContent.transform); 
            upgradesSpawnIndex++;
            if (UpgradesScriptableObjectsArray[upgradesSpawnIndex] == null)
                break;
        }
    }

    public void CalculateGCPS()
    {
        goatCoinPerSecond = 0;
        for (int i = 0; i < ShopContent.transform.childCount; i++)
        {
            goatCoinPerSecond += ShopContent.transform.GetChild(i).GetComponent<ShopItem>().QuantityOfBongoats * ShopContent.transform.GetChild(i).GetComponent<ShopItem>().GCPS;
        }
        AddMoney(goatCoinPerSecond / 50);
    }

    public void multiplyClicker(float multiplyer)
    {
        GoatCoinPerClick *= multiplyer;
    }

    public void SetActiveNextBuyPlaceItem(GameObject CurrentUpgrade)
    {
        if (CurrentUpgrade.transform.GetSiblingIndex() >= CurrentUpgrade.transform.parent.transform.childCount - 1)
            return;

        CurrentUpgrade.transform.parent.transform.GetChild(CurrentUpgrade.transform.GetSiblingIndex() + 1).gameObject.SetActive(true);
    }

    public void SaveData()
    {
        saveLoad.GoatCoin = GoatCoin;
        saveLoad.goatCoinPerSecond = goatCoinPerSecond;

        //shop items
        for (int i = 0; i <= ShopItems.Length - 1 && ShopItems[i] != null; i++)
        {
            Debug.Log(i + ", " + ShopItems.Length);
            ShopItems[i].GetComponent<ShopItem>().buy(saveLoad.shopItems[i], true);
        }
        // upgrades
        for (int i = 0; i <= Upgrades.Length - 1 && Upgrades[i] != null; i++)
        {   
            Debug.Log(i + ", " + Upgrades.Length);
            UpgradeItem tempUpgrade = Upgrades[i].GetComponent<UpgradeItem>();

            switch (saveLoad.upgrades[i])
            {
            case 1:
                tempUpgrade.Buy(true);
                break;
            case 2:
                tempUpgrade.Buy(true);
                tempUpgrade.onClick();;
                break;
            }
        }

        saveLoad.SaveGame();

    }

    public void LoadData()
    {
        GoatCoin = saveLoad.GoatCoin;
        goatCoinPerSecond = saveLoad.goatCoinPerSecond;

        for (int i = 0; i <= ShopItems.Length - 1 && ShopItems[i] != null; i++)
        {
            ShopItems[i].GetComponent<ShopItem>().buy(saveLoad.shopItems[i], true);
        }

        for (int i = 0; i <= Upgrades.Length - 1 && Upgrades[i] != null; i++)
        {   
            UpgradeItem tempUpgrade = Upgrades[i].GetComponent<UpgradeItem>();

            switch (saveLoad.upgrades[i])
            {
            case 1:
                tempUpgrade.Buy(true);
                break;
            case 2:
                tempUpgrade.Buy(true);
                tempUpgrade.onClick();;
                break;
            }
        }
    }

    /*// money
        MoneyManager.GoatCoin = savedata.GoatCoin;
        // goatcoin per second
        MoneyManager.goatCoinPerSecond = savedata.GCPS;
        // shop
        for (int i = 0; i <= MoneyManager.ShopItems.Length && MoneyManager.ShopItems[i] != null; i++)
        {
            MoneyManager.ShopItems[i].GetComponent<ShopItem>().buy(savedata.shopItems[i], true);
        }
        // upgrades
        for (int i = 0; i <= MoneyManager.Upgrades.Length && MoneyManager.Upgrades[i] != null; i++)
        {   
            UpgradeItem tempUpgrade = MoneyManager.Upgrades[i].GetComponent<UpgradeItem>();

            switch (savedata.upgrades[i])
            {
            case 1:
                tempUpgrade.Buy(true);
                break;
            case 2:
                tempUpgrade.Buy(true);
                tempUpgrade.onClick();;
                break;
            }
        }
        */
}
