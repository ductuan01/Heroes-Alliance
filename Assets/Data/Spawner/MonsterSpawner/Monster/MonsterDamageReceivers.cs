/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamageReceivers : damagerecer
{
    [Header("Monster")]

    [SerializeField] protected MonsterMonsterCtrl monsterCtrl;
    [SerializeField] protected int MonsterLevel;
    [SerializeField] protected float MonsterDR;     //DamageReduce(%)
    [SerializeField] protected float MonsterExp;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMonsterCtrl();
    }

    protected virtual void LoadMonsterCtrl()
    {
        if (monsterCtrl != null) return;
        this.monsterCtrl = transform.parent.GetComponent<MonsterMonsterCtrl>();
        Debug.LogWarning(transform.name + ": LoadMonsterCtrl", gameObject);
    }

    protected override void Reborn()
    {
        this.MonsterLevel = this.monsterCtrl.MonsterSO.MonsterLevel;
        this.hpMax = this.monsterCtrl.MonsterSO.hpMax;
        this.MonsterDR = this.monsterCtrl.MonsterSO.MonsterDR;
        this.MonsterExp = this.monsterCtrl.MonsterSO.MonsterExp;
        base.Reborn();
    }

    public override void Deduct(float amount)
    {
        Debug.Log("receive: " + amount * (1 - this.MonsterDR));
        this.hp -= amount * (1 - this.MonsterDR);
        if (this.hp < 0) this.hp = 0;
        this.CheckIsDead();
    }

    protected override void OnDead()
    {
        this.OnDeadDrop();
        Debug.Log("11111111111: " + 100);
        ExperienceManager.Instance.AddExperience(100);
        MonsterSpawner.Instance.Despawn(transform.parent);
    }

    public virtual void OnDeadDrop()
    {
        Vector3 dropPos = transform.position;
        Quaternion dropRot = transform.rotation;
        //ItemDropSpawner.Instance.Drop(this.monsterCtrl.MonsterSO.dropList, dropPos, dropRot);
        //EquipmentSpawner.Instance.MonsterDrop(this.monsterCtrl.MonsterSO, dropPos, dropRot);
        UseSpawner.Instance.MonsterDrop(this.monsterCtrl.MonsterSO, dropPos, dropRot);
        //EtcSpawner.Instance.MonsterDrop(this.monsterCtrl.MonsterSO, dropPos, dropRot);
        NTDSpawner.Instance.MonsterDrop(this.monsterCtrl.MonsterSO, dropPos, dropRot);
    }
}
*/
//EquipmentSpawner.Instance.MonsterDrop(this.monsterCtrl.MonsterSO, dropPos, dropRot);