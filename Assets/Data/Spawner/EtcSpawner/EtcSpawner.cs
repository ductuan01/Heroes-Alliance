using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtcSpawner : Spawner
{
    private static EtcSpawner _instance;
    public static EtcSpawner Instance => _instance;

    protected override void Awake()
    {
        base.Awake();
        if (EtcSpawner._instance != null) Debug.LogError("Only 1 EtcSpawner allow to exist");
        EtcSpawner._instance = this;
    }
    
    public virtual void MonsterDrop(MonsterSO monsterSO, Vector3 pos, Quaternion rot)
    {
        List<EtcDropList> etcs = monsterSO.etcDropList;
        foreach (EtcDropList etc in etcs)
        {
            EtcCode etcName = etc.etcProfile.etcCode;

            Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), 0f, 0f);
            Vector3 targetPosition = pos + randomOffset;

            Transform etcDrop = this.Spawn(etcName.ToString(), pos, rot);
            if (etcDrop == null) return;
            etcDrop.gameObject.SetActive(true);
            StartCoroutine(FlyItem(etcDrop, targetPosition));
        }
    }
}
