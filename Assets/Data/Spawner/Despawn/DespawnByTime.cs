using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByTime : Despawn
{
    [SerializeField] protected float timeLimit = 0.1f;
    [SerializeField] protected float timer = 0f;

    protected override bool CanDespawn()
    {
        timer += Time.deltaTime;
        if (this.timer < this.timeLimit) return false;
        timer = 0f;
        return true;
    }
}
