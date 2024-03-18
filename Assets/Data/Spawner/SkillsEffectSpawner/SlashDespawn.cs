using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashDespawn : DespawnByTime
{
    public override void DespawnObject()
    {
        SkillEffectSpawner.Instance.Despawn(transform.parent);
    }
}
