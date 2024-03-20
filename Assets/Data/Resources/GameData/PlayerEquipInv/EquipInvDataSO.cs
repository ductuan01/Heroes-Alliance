using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipInvData", menuName = "SO/GameDataSO/EquipInv")]
public class EquipInvDataSO : ScriptableObject
{
    public List<PlayerEquipInvInfo> EquipInvData;
}
