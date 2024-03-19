using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActiveSkill
{
    public int levelSkill;
    public int MPCost;
    public int HPCost;
    public int DamageSkill; // % rate
    public int maxEnemyHit; // number enemy can hit
    public int maxHit;

    public float Force; // for war leap

    public string infoSkill = "";
}
