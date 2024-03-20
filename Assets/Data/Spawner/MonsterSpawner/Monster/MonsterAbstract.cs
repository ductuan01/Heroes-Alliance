using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterAbstract : SecondMonoBehaviour
{
    [Header("MonsterAbstract")]

    [SerializeField] private MonsterCtrl _monsterCtrl;
    public MonsterCtrl MonsterCtrl => _monsterCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMonsterCtrl();
    }

    protected virtual void LoadMonsterCtrl()
    {
        if (_monsterCtrl != null) return;
        this._monsterCtrl = transform.GetComponentInParent<MonsterCtrl>();
        Debug.LogWarning(transform.name + ": LoadMonsterCtrl", gameObject);
    }
}
