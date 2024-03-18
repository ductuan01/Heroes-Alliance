using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonsterAbstract
{
    [SerializeField] private Animator _monsterAnim;

    private static float _lockedTill = 0.1f;
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
    }

    private void LoadAnimator()
    {
        if (this._monsterAnim != null) return;
        this._monsterAnim = transform.GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    private void FixedUpdate()
    {
        GetState();
    }

    private void GetState()
    {
        if (Time.time < _lockedTill) return;

        if (MonsterCtrl.MonsterStats.isDead)
        {
            this._monsterAnim.SetTrigger("Dead");
            LockState(1.5f);
        }

        if (!MonsterCtrl.MonsterStats.isDead)
        {
            if (MonsterCtrl.MonsterAttack.IsAttacking)
            {
                this._monsterAnim.SetTrigger("Attack");
            }
            else
            {
                this._monsterAnim.SetFloat("Run", Mathf.Abs(MonsterCtrl.MonsterMovement.xInput));
            }
        }

        static void LockState(float t)
        {
            _lockedTill = Time.time + t;
        }
    }
}
