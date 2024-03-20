using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatsData", menuName = "SO/GameDataSO/Stats")]

public class StatsDataSO : ScriptableObject
{
    public int abilityPoint;
    public int currentHP;
    public int maxHP;
    public int currentMP;
    public int maxMP;
    public int baseSTR;
    public int baseDEX;
    public int baseINT;
    public int baseLUK;
}
