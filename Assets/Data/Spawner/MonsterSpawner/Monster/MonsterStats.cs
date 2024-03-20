using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStats : MonsterAbstract
{
    [Header("Monster Stats")]
    [SerializeField] private string _monsterName;
    [SerializeField] private int _monsterLevel;
    [SerializeField] private int _damage;
    public int Damage => _damage;

    [SerializeField] private int _maxHP;
    [SerializeField] private int _currentHP = 0;
    [SerializeField] private int _maxMP;
    [SerializeField] private int _currentMP = 0;
    [SerializeField] private int _EXP;
    [SerializeField] private int _Def = 0;

    [SerializeField] private bool _isDead = false;
    public bool isDead => _isDead;

    protected override void OnEnable()
    {
        base.OnEnable();
        this.LoadMonsterStats();
    }

    private void LoadMonsterStats()
    {
        this._isDead = false;
        this._monsterName = this.MonsterCtrl.MonsterSO.monsterName;
        this._monsterLevel = this.MonsterCtrl.MonsterSO.MonsterLevel;
        this._damage = this.MonsterCtrl.MonsterSO.damage;
        this._maxHP = this.MonsterCtrl.MonsterSO.hpMax;
        this._currentHP = this.MonsterCtrl.MonsterSO.hpMax;
        this._maxMP = this.MonsterCtrl.MonsterSO.hpMax;
        this._currentMP = this.MonsterCtrl.MonsterSO.hpMax;
        this._EXP = (int)this.MonsterCtrl.MonsterSO.MonsterExp;

        this.MonsterCtrl._monsterName.SetText(this._monsterName);
        this.MonsterCtrl._monsterLevel.SetText("Lv. " + this._monsterLevel.ToString());
        this.SetHpBar();
    }

    /*public void RiseHP(int amount)
    {
        this._currentHP += amount;
        if (this._currentHP > this._maxHP) this._currentHP = this._maxHP;
    }*/

    public virtual bool ReduceHP(int amount)
    {
        if (this.IsDead()) return false;
        this._currentHP -= amount;
        if (this._currentHP < 0) this._currentHP = 0;
        this.SetHpBar();
        this.CheckIsDead();
        return true;
    }

    private void SetHpBar()
    {
        float value = (float)(this._currentHP) / (float)(this._maxHP);
        this.MonsterCtrl._slider.value = value;
    }    

    protected virtual void CheckIsDead()
    {
        if (!this.IsDead()) return;
        this._isDead = true;
        this.OnDead();
    }

    protected virtual bool IsDead()
    {
        return this._currentHP <= 0;
    }

    protected virtual void OnDead()
    {
        ExperienceManager.Instance.AddExperience(this._EXP);
        MonsterCtrl.MonsterMovement._bIsStop = true;
        MonsterCtrl.MonsterMovement._bIsChaseMode = false;
        MonsterCtrl.MonsterDrop.ItemsDrop();
        StartCoroutine(Dead());
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(2f);
        MonsterSpawner.Instance.Despawn(transform.parent);
    }
}
