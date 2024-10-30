using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Upgrade", menuName ="Upgrade")]
public class UpgradesSO : ScriptableObject
{
    public Sprite artwork;
    public Sprite SpriteOnBongoat;
    [Space]
    public string UpgradeName;
    public float cost;
    public float multiplyer;
    public string Description;
}
