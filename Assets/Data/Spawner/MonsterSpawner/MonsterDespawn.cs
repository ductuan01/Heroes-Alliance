using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDespawn : DespawnByTime
{
    public override void DespawnObject()
    {
        MonsterSpawner.Instance.Despawn(transform.parent);
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.timeLimit = 60f;
    }
}
