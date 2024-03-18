using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "SO/Monster")]
public class MonsterSO : ScriptableObject
{
    public int MonsterLevel = 1;
    public string monsterName = "Monster_1";
    public int damage = 5;
    public int hpMax = 5;
    public float MonsterDR; // rate %
    public float MonsterExp;
    public List<EquipmentDropList> equipmentDropList;
    public List<UseDropList> useDropList;
    public List<EtcDropList> etcDropList;
    public List<NTDDropList> ntdDropList;
}
