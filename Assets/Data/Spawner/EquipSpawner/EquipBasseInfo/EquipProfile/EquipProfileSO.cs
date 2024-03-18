using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemProfileSO", menuName = "SO/EquipmentProfileSO")]

public class EquipProfileSO : ScriptableObject
{
    public EquipmentName equipmentName = EquipmentName.NoName;

    public ClassType classType = ClassType.NoType;
    public EquipmentType equipmentType = EquipmentType.NoType;

    public int REQLEV;
    public int REQSTR;
    public int REQDEX;
    public int REQINT;
    public int REQLUK;

    public int STR;
    public int DEX;
    public int INT;
    public int LUK;

    public int MaxHP;
    public int MaxMP;

    public int AttPower;
    public int MattPower;

    public int RemainingEnhancements;
    public int defaultMaxStack = 1;

    public float radiusCollider;
    public Sprite equipSprite;

    //public List<ItemRecipe> upgradeLevels;
}
