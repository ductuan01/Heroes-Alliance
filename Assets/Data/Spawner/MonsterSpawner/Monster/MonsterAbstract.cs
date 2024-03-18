/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterAbstract : SecondMonoBehaviour
{
    [Header("Monster Abstract")]
    [SerializeField] protected MonsterMonsterCtrl monsterCtrl;
    public MonsterMonsterCtrl MonsterCtrl => monsterCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMonsterCtrl();
    }

    protected virtual void LoadMonsterCtrl()
    {
        if (monsterCtrl != null) return;
        this.monsterCtrl = transform.GetComponentInParent<MonsterMonsterCtrl>();
        Debug.LogWarning(transform.name + ": LoadMonsterCtrl", gameObject);
    }
}
*/