using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Shop Item", menuName ="ShopItem")]
public class ShopItemSO : ScriptableObject
{
    public Sprite artwork;
    public string SOname;
    public float cost;
    public float GCPS;
    int quantity;
    public string Description;
}
