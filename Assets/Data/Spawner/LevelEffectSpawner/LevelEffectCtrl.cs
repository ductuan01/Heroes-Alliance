using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEffectCtrl : SecondMonoBehaviour
{
    private float despwanTimer;
    private int _speed = 100;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.despwanTimer = 1f;
    }

    private void FixedUpdate()
    {
        despwanTimer -= Time.fixedDeltaTime;
        if (despwanTimer < 0)
        {
            ImpactSpawner.Instance.Despawn(transform);
        }
        this.followPlayer();
    }

    private void followPlayer()
    {
        Vector3 targetPos = PlayerCtrl.Instance.transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime * this._speed);
    }
}
