using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTDSpawner : Spawner
{
    private static NTDSpawner _instance;
    public static NTDSpawner Instance => _instance;

    protected override void Awake()
    {
        base.Awake();
        if (NTDSpawner._instance != null) Debug.LogError("Only 1 NTDSpawner allow to exist");
        NTDSpawner._instance = this;
    }
/*
    public virtual void MonsterDrop(MonsterSO monsterSO, Vector3 pos, Quaternion rot)
    {
        Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), 0f, 0f);
        Vector3 targetPosition = pos + randomOffset;

        NTDProfileSO ntdProfile = monsterSO.ntdDropList.;
        NTDType ntdType = ntdProfile.ntdType;
        Transform ntdDrop = this.Spawn(ntdType.ToString(), pos, rot);
        if (ntdDrop == null) return;
        //ntdDrop.GetComponent<NTDCtrl>().NTDInfo.SetNTDAmount(Random.Range(ntdProfile.minNTD, ntdProfile.maxNTD));
        ntdDrop.gameObject.SetActive(true);

        StartCoroutine(FlyItem(ntdDrop, targetPosition));
    }*/
}
