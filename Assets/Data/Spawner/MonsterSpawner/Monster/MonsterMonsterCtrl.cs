/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMonsterCtrl : SecondMonoBehaviour
{
    [SerializeField] protected Animator animator;
    public Animator Animator => animator;

    [SerializeField] protected Transform model;
    public Transform Model => model;

    [SerializeField] protected DamageSender damageSender;
    public DamageSender DamageSender => damageSender;

    [SerializeField] protected MonsterSO monsterSO;
    public MonsterSO MonsterSO => monsterSO;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
        this.LoadModel();
        this.LoadDamageSender();
        this.LoadMonsterSO();
    }

    protected virtual void LoadMonsterSO()
    {
        if (this.monsterSO != null) return;
        string resPath = "Monster/" + transform.name;
        this.monsterSO = Resources.Load<MonsterSO>(resPath);
        Debug.LogWarning(transform.name + ": LoadMonsterSO" + resPath, gameObject);
    }

    protected virtual void LoadDamageSender()
    {
        if (this.damageSender != null) return;
        this.damageSender = transform.GetComponentInChildren<DamageSender>();
        Debug.LogWarning(transform.name + ": LoadDamageSender", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = transform.Find("Model").GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model");
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }
}
*/