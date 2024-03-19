using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashBlastEffectCtrl : SecondMonoBehaviour
{
    private float despwanTimer;
    private Transform _player;

    private int _speed = 100;

    protected override void Start()
    {
        base.Start();
        this._player = PlayerCtrl.Instance.GetComponent<Transform>();
    }

    public void Setup(float despwanTimer)
    {
        this.despwanTimer = despwanTimer;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.despwanTimer = 1f;
    }
    private void Update()
    {
        despwanTimer -= Time.deltaTime;
        if (despwanTimer < 0)
        {
            SkillEffectSpawner.Instance.Despawn(transform);
        }
        this.followPlayer();
    }

    private void followPlayer()
    {
        if (this._player == null) return;
        Vector3 targetPos = this._player.position;
        targetPos.x += PlayerCtrl.Instance.PlayerMovement.FacingDirection;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime * this._speed);
    }
}
