using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemProfileSO", menuName = "SO/SkillProfileSO")]

public class SkillProfileSO : ScriptableObject
{
    public string skillName = "";

    public SkillType skillType = SkillType.NoType;
    public SkillCode skillCode = SkillCode.NoCode;

    public int masterLevel = 5;

    public string Description;

    // ActiveSkill
    public List<ActiveSkill> InfoActiveSkill;

    public List<PassiveSkill> InfoPassiveSkill;
    // Supportive and Passive
    public int recoverHP; // % rate
    public int recoverMP; // % rate
    public int Duration;
    public int timeRecover;

    public int increasesSTR;
    public int increasesDEX;
    public int increasesINT;
    public int increasesLUK;

    public int increasesDef;
    public int increasesAtt;

    public int increasesMaxHP;
    public int increasesMaxMP;

    public Sprite skillSprite;
}
