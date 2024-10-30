using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Furniture", menuName ="Furniture")]
public class FurnitureScriptableObject : ScriptableObject
{
    public Sprite sprite;
    public new string name;
    public float cost;
    public float boostAmountInPercentage;
    public string Description;
    public int height;
    public int width;
}
