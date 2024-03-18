using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemProfileSO", menuName = "SO/EtcProfileSO")]

public class EtcProfileSO : ScriptableObject
{
    public string etcName = "This is name";
    public EtcType etcType = EtcType.NoType;
    public EtcCode etcCode = EtcCode.NoCode;

    public int Amount = 1;
    public int defaultMaxStack = 5;
    public Sprite etcSprite;
    public float radiusCollider = 0.35f;
    public string etcDescription = "This is description";
}
