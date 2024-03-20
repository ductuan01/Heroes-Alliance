using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonsterAbstract
{
    [SerializeField] private Animator _monsterAnim;

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
        if (MonsterCtrl.MonsterStats.isDead)
        {
            this._monsterAnim.SetTrigger("Dead");
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
    }
}
