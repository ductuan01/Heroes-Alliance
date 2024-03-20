using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemsData", menuName = "SO/GameDataSO/Items")]

public class ItemsDataSO : ScriptableObject
{
    public List<EquipInformation> equipsData;
    public List<UseInformation> usesData;
    public List<EtcInformation> etcsData;
    public int NTD;
}
