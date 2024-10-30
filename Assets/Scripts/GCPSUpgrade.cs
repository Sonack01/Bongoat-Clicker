using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GCPSUpgrade : MonoBehaviour
{

    public ShopItemSO shopItemSO;

    [SerializeField] GameObject MoneyManager;
    [SerializeField] MoneyManager MoneyManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        MoneyManager = GameObject.Find("GameManager");
        MoneyManagerScript = MoneyManager.GetComponent<MoneyManager>();

        //shopItemSO = ;
        MoneyManagerScript.shopSpawnIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
