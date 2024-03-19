using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerRandom : SecondMonoBehaviour
{
    [SerializeField] protected MonsterSpawnerCtrl monsterSpawnerCtrl;
    public MonsterSpawnerCtrl MonsterSpawnerCtrl => monsterSpawnerCtrl;

    [SerializeField] protected float randomDelay = 1f;
    [SerializeField] protected float randomTimer = 0f;
    [SerializeField] protected float randomLimit = 4f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMonsterSpawnerCtrl();
    }

    protected virtual void LoadMonsterSpawnerCtrl()
    {
        if (this.monsterSpawnerCtrl != null) return;
        this.monsterSpawnerCtrl = GetComponent<MonsterSpawnerCtrl>();
        Debug.LogWarning(transform.name + ": LoadMonsterCtrl", gameObject);
    }

    private void FixedUpdate()
    {
        this.MonsterSpawning();
    }

    protected virtual void MonsterSpawning()
    {
        if (this.RandomReachLimit()) return;

        this.randomTimer += Time.fixedDeltaTime;
        if (this.randomTimer < this.randomDelay) return;
        this.randomTimer = 0;

        Transform randomPoint = this.monsterSpawnerCtrl.MonsterSpawnerPoints.GetRandomPoint();
        Vector3 pos = randomPoint.position;
        pos.z = 0;
        //Quaternion rot = transform.rotation;

        Transform obj = this.monsterSpawnerCtrl.MonsterSpawner.Spawn(MonsterSpawner.monster2, pos, Quaternion.identity);
        obj.gameObject.SetActive(true);
    }

    protected virtual bool RandomReachLimit()
    {
        int currentMonster = this.MonsterSpawnerCtrl.MonsterSpawner.SpawnedCount;
        return currentMonster >= this.randomLimit;
    }
}