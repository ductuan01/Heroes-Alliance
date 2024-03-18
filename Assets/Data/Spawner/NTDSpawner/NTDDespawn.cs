using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NTDDespawn : DespawnByTime
{
    public override void DespawnObject()
    {
        NTDSpawner.Instance.Despawn(transform.parent);
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.timeLimit = 100f;
    }
}
