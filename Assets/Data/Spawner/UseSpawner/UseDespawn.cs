using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseDespawn : DespawnByTime
{
    protected override void OnEnable()
    {
        this.timer = 0;
    }

    public override void DespawnObject()
    {
        EquipSpawner.Instance.Despawn(transform.parent);
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.timeLimit = 100f;
    }
}
