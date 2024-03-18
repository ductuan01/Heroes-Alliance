using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerCtrl : SecondMonoBehaviour
{
    [SerializeField] protected MonsterSpawner monsterSpawner;
    public MonsterSpawner MonsterSpawner => monsterSpawner;

    [SerializeField] protected MonsterSpawnerPoints monsterSpawnerPoints;
    public MonsterSpawnerPoints MonsterSpawnerPoints => monsterSpawnerPoints;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMonsterSpawner();
        this.LoadMonsterSpawnerPoint();
    }

    protected virtual void LoadMonsterSpawner()
    {
        if (monsterSpawner != null) return;
        this.monsterSpawner = GetComponent<MonsterSpawner>();
        Debug.LogWarning(transform.name + ": LoadMonsterSpawner", gameObject);
    }

    protected virtual void LoadMonsterSpawnerPoint()
    {
        if (this.monsterSpawnerPoints != null) return;
        this.monsterSpawnerPoints = Transform.FindObjectOfType<MonsterSpawnerPoints>();
        Debug.LogWarning(transform.name + ": LoadMonsterSpawnerPoint", gameObject);
    }
}
