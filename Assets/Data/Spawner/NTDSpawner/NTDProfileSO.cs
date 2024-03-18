using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemProfileSO", menuName = "SO/NTDSO")]

public class NTDProfileSO : ScriptableObject
{
    public NTDType ntdType = NTDType.NoType;
    public int minNTD;
    public int maxNTD;
    public float dropRate;
    public float radiusCollider = 0.25f;
    public Sprite coinSprite;
}
