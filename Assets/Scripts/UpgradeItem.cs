using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Mathematics;
using System.Text;
using System;

public class UpgradeItem : MonoBehaviour
{

    public UpgradesSO upgradesScriptableObject;

    Image Icon;
    TextMeshProUGUI Costtxt;
    TextMeshProUGUI GCPStxt;
    TextMeshProUGUI Nametxt;
    TextMeshProUGUI Quantitytxt;

    public Sprite artwork;
    public Sprite SpriteOnBongoat;

    string UpgradeItemName = "Placeholder Name";
    float cost = 0f;
    float multiplier = 0;
    public int quantity = 0;

    public bool isBrought = false;
    public bool isEquipped = false;
    GameObject equippedTxt;

    GameObject accessory;
    GameObject placeholder_bongoat;

    [SerializeField] GameObject accessoryLocation;
    [SerializeField] GameObject MoneyManager;
    [SerializeField] MoneyManager MoneyManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        MoneyManager = GameObject.Find("GameManager");
        MoneyManagerScript = MoneyManager.GetComponent<MoneyManager>();

        placeholder_bongoat = Resources.Load<GameObject>("Prefabs/placeholder_bongoat");

        upgradesScriptableObject = MoneyManagerScript.UpgradesScriptableObjectsArray[gameObject.transform.GetSiblingIndex()];

        artwork = upgradesScriptableObject.artwork;
        name = upgradesScriptableObject.UpgradeName;
        cost = upgradesScriptableObject.cost;
        multiplier = upgradesScriptableObject.multiplyer;

        Icon = gameObject.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        Costtxt = gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        equippedTxt = gameObject.transform.GetChild(2).gameObject;

        Costtxt.text = cost.ToString() + " GC";
        Icon.sprite = artwork;
        SpriteOnBongoat = upgradesScriptableObject.SpriteOnBongoat;

        accessoryLocation = GameObject.Find("Bongoat Customise");

        if (gameObject.transform.GetSiblingIndex() != 0)
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (MoneyManagerScript.GoatCoin >= cost)
        {
            Costtxt.color = Color.green;
        }
        else
        {
            Costtxt.color = Color.red;
        }
    }

    public void onClick()
    {
        Buy(false);

        if (isEquipped)
            equip(false);
        else
            equip(true);

        
    }

    public void Buy(bool isFree)
    {
        if (isBrought)
            return;

        float Temp = cost;

        if (isFree)
            Temp = 0;
        
        
        if (Temp <= MoneyManagerScript.GoatCoin)
        {
            MoneyManagerScript.RemoveMoney(Temp);
            isBrought = true;
            Destroy(Costtxt);
            MoneyManagerScript.multiplyClicker(multiplier);
            MoneyManagerScript.SetActiveNextBuyPlaceItem(gameObject);
        }
    }

    public void equip(bool equipped)
    {
        if (!isBrought)
            return;

        if (!isEquipped)
        {
            Icon.color = Color.white;
            accessory = Instantiate(placeholder_bongoat, accessoryLocation.transform.position, quaternion.identity, accessoryLocation.transform);
            accessory.GetComponent<Image>().sprite = SpriteOnBongoat;
        }   
        else
        {
            Icon.color = Color.gray;
            Destroy(accessory);
        }

        equippedTxt.SetActive(equipped);
        isEquipped = equipped;
    }

    void TooltipText()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<size=24>" + name + "<line-height=135%><br>");
        sb.Append("<line-height=100%><size=18>Clicker multiplier: <u><size=22>" + multiplier + "x<size=18></u><br>");
        sb.Append("<line-height=125%>Current GC per Click: <u><size=22>" + MoneyManagerScript.GoatCoinPerClick);
        
        gameObject.GetComponent<SpawnTooltip>().SetTooltipText(sb.ToString(), 50);
    }
    
    public void OnCursorHover()
    {
        TooltipText();
    }
}
