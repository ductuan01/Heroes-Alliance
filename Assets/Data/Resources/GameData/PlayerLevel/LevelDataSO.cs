using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "SO/GameDataSO/Level")]

public class LevelDataSO : ScriptableObject
{
    public int currentLevel;
    public int currentExperience;
    public int maxExperience;
}
