using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillsData", menuName = "SO/GameDataSO/Skills")]

public class SkillsDataSO : ScriptableObject
{
    public int skillPoint;
    public List<SkillInfoOfData> job1;
}
