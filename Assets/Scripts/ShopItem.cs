using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Text;

public class ShopItem : MonoBehaviour
{
    public ShopItemSO shopItemScriptableObject;

    [SerializeField] Image Icon;
    [SerializeField] TextMeshProUGUI Costtxt;
    [SerializeField] TextMeshProUGUI GCPStxt;
    [SerializeField] TextMeshProUGUI Nametxt;
    [SerializeField] TextMeshProUGUI Quantitytxt;

    public Sprite artwork;
    public string shopItemName = "Placeholder Name";
    public float cost = 0f;
    public float GCPS = 0f;
    public int QuantityOfBongoats = 0;
    public string Description;

    [SerializeField] GameObject MoneyManager;
    MoneyManager MoneyManagerScript;

    [SerializeField] int shopItemIndex;

    int[] QuantityBatchIncrease = {10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 125, 150, 175, 200};
    int QBIIndex = 0;

    bool isHover = false;

    [SerializeField] GameObject BottomPanel;
    public int BulkBuyAmount;
    int canBuyQuantity = 1;

    /*

        try making a way of setting this up when instantiated

        the variables in this script will have to be tracked by the MoneyManager so the maths can be done for the GCPS

        dont forget to set it active when everything is complete

    */

    private void Awake()
    {
        MoneyManager = GameObject.Find("GameManager");
        MoneyManagerScript = MoneyManager.GetComponent<MoneyManager>();
        shopItemScriptableObject = MoneyManagerScript.ShopItemsScriptableObjectsArray[gameObject.transform.GetSiblingIndex()];

        artwork = shopItemScriptableObject.artwork;
        name = shopItemScriptableObject.SOname;
        cost = shopItemScriptableObject.cost;
        GCPS = shopItemScriptableObject.GCPS;
        Description = shopItemScriptableObject.Description;
        
        Icon = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        Costtxt = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        GCPStxt = gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        Nametxt = gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        Quantitytxt = gameObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>();

        Icon.sprite = artwork;

        Nametxt.text = name;

        UpdateText(BulkBuyAmount);

        if (gameObject.transform.GetSiblingIndex() != 0)
            gameObject.SetActive(false);

        BottomPanel = GameObject.Find("BottomPanel");
    }

    private void Update() 
    {
        if (MoneyManagerScript.GoatCoin >= cost)
        {
            Costtxt.color = Color.green;
        }
        else
        {
            Costtxt.color = Color.red;
        }   

        if (Input.GetMouseButtonDown(1) && isHover)
        {
            Sell(BulkBuyAmount);
            //UpdateText(BulkBuyAmount); 
            //GhostPriceUpdateText(BulkBuyAmount);
        }

        BulkBuyAmount = BottomPanel.GetComponent<BulkBuyController>().BuyQuantity;

        UpdateText(BulkBuyAmount);
    }

    public void onLeftClick()
    {
        buy(BulkBuyAmount);
        //UpdateText(BulkBuyAmount);
        //GhostPriceUpdateText(BulkBuyAmount);
    }

    public void buy(int BuyAmount)
    {
        for (int i = 0; i < BuyAmount; i++)
        {
            if (cost <= MoneyManagerScript.GoatCoin)
            {
                //where my mass buying at
                QuantityOfBongoats++;
                MoneyManagerScript.RemoveMoney(cost);

                if (QuantityOfBongoats <= 1)
                    MoneyManagerScript.SetActiveNextBuyPlaceItem(gameObject);

                PositivePriceAdjustment();
                GCPSAdjustment();
            }
        }
    }

    public void buy(int BuyAmount, bool isFree)
    {
        float temp = cost;
        if (isFree)
            temp = 0;

        for (int i = 0; i < BuyAmount; i++)
        {
            if (temp <= MoneyManagerScript.GoatCoin)
            {
                //where my mass buying at
                QuantityOfBongoats++;
                MoneyManagerScript.RemoveMoney(temp);

                if (QuantityOfBongoats <= 1)
                    MoneyManagerScript.SetActiveNextBuyPlaceItem(gameObject);

                PositivePriceAdjustment();
                GCPSAdjustment();
            }
        }
    }

    void Sell(float SellAmount)
    {
        
        for (int i = 0; i < SellAmount; i++)
        {
            if (QuantityOfBongoats >= 1)
            {
                QuantityOfBongoats--;
                MoneyManagerScript.AddMoney(cost * 0.75f);

                NegativePriceAdjustment();
                GCPSAdjustment();
            }
        } 
    }

    void PositivePriceAdjustment()
    {
        cost = cost * 1.15f;
    }

    void UpdateText(int quantity)
    {
        // someone take sandpaper to my brain to make me smarter

        float TOTAL_GHOST_COST = cost;
        float TEMP_COST = cost;
        
        for (int i = 1; i < quantity + 1; i++)
        {
            //Debug.Log(gameObject.name + "| " + i + " : " +TOTAL_GHOST_COST + " increased by " + TEMP_COST);
            TOTAL_GHOST_COST += TEMP_COST;

            TEMP_COST = TEMP_COST * 1.15f;
            canBuyQuantity = i;
        }
        
        if (quantity > 1)
            Costtxt.text = canBuyQuantity +"x $" + Math.Round(TOTAL_GHOST_COST) + " GC";
        else
            Costtxt.text = "$" + Math.Round(cost) + " GC";

        Quantitytxt.text = QuantityOfBongoats.ToString();
        GCPStxt.text = Math.Round(GCPS, 3) + " GC/s";
    }

    /*
    void updateText() // DEPRICATED //
    {
        Quantitytxt.text = QuantityOfBongoats.ToString();

        Costtxt.text = "$" + Math.Round(cost) + " GC";
        GCPStxt.text = Math.Round(GCPS, 3) + " GC/s";
    }
    */

    void NegativePriceAdjustment()
    {
        cost = cost / 115 * 100;
    }

    void GCPSAdjustment()
    {
        if (QuantityOfBongoats >= QuantityBatchIncrease[QBIIndex])
        {
            QBIIndex++; 
            GCPS = GCPS * 1.75f;
        }
    }

    void TooltipText()
    {
        string percentageOfGCPS = Math.Round(QuantityOfBongoats * GCPS / MoneyManagerScript.goatCoinPerSecond * 100, 1).ToString();

        StringBuilder sb = new StringBuilder();
        sb.Append("<line-height=135%><size=24>" + name + "<br>");

        if (percentageOfGCPS == "NaN")
            sb.Append("<align=right><size=16>contributes <u>" + QuantityOfBongoats * GCPS + "GC </u>or <u>0%</u> of GCPS<br>");
        else
            sb.Append("<align=right><size=16>contributes <u>" + QuantityOfBongoats * GCPS + "GC </u>or <u>" + percentageOfGCPS + "%</u> of GCPS<br>");

        sb.Append("<align=left><size=20><line-height=100%><u>Description:</u><br>");
        sb.Append("<size=16>" + Description + "<line-height=175%><br>");

        if (BulkBuyAmount > 100)
            sb.Append("<size=16>current buy amount: <size=20><u>MAX<br>");
        else
            sb.Append("<size=16>current buy amount: <size=20><u>" + BulkBuyAmount + "<br>");



        gameObject.GetComponent<SpawnTooltip>().SetTooltipText(sb.ToString(), 50);
    }
    
    public void OnCursorHover()
    {
        TooltipText();
        isHover = true;
    }

    public void OnCursorLeave()
    {
        isHover = false;
    }
}
