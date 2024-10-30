using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulkBuyController : MonoBehaviour
{
    public int BuyQuantity = 1;

    //public bool BulkBuyFunctionRan = false;

    /*
    public void ONE()
    {
        BuyQuantity = 1;
    }

    public void TEN()
    {
        BuyQuantity = 10;
    }

    public void ONEHUNDRED()
    {
        BuyQuantity = 100;
    }

    public void MAX()
    {
        BuyQuantity = 10000;
    }*/

    public void SetBuyQuantity(int quantity)
    {
        BuyQuantity = quantity;
        //BulkBuyFunctionRan = true;
    }

}
