using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemProfileSO", menuName = "SO/UseProfileSO")]

public class UseProfileSO : ScriptableObject
{
    public string useName = "This is name";
    public UseType useType = UseType.NoType;
    public UseCode useCode = UseCode.NoCode;

    public int RecoversHP;
    public int RecoversMP;

    //public ScrollName scrollName = ScrollName.NoName;
    public int STR;
    public int DEX;
    public int INT;
    public int LUK;

    public int MaxHP;
    public int MaxMP;

    public int AttPower;
    public int MattPower;
    public float Rate; //rate success (0-1)
    public int Amount = 1;
    public int defaultMaxStack = 5;

    public float radiusCollider;
    public Sprite useSprite;
    public string useDescription;
}
