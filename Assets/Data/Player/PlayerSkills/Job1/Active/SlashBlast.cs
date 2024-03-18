using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashBlast : PlayerSkillsAbstract
{
    private static SlashBlast _instance;
    public static SlashBlast Instance => _instance;

    [SerializeField] private SkillInfo _skillInfo;

    [SerializeField] private Transform _skillRange;

    [SerializeField] private int _maxEnemyHit = 2;

    [SerializeField] private int _maxHit = 3;

    [SerializeField] private float _speed = 4f;

    private Vector3 newPos;

    [SerializeField] private LayerMask _whatIsDamageAble;

    protected override void Awake()
    {
        base.Awake();
        if (SlashBlast._instance != null) Debug.LogError("Only 1 SlashBlast allow to exist");
        SlashBlast._instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSkillInfo();
        this.LoadSkillRange();
        this._whatIsDamageAble = LayerMask.GetMask("Enemy");
    }
    private void LoadSkillInfo()
    {
        if (this._skillInfo != null) return;
        this._skillInfo = transform.GetComponentInChildren<SkillInfo>();
        Debug.LogWarning(transform.name + ": LoadSkillInfo", gameObject);
    }

    private void LoadSkillRange()
    {
        if (this._skillRange != null) return;
        this._skillRange = transform.Find("Range");
        Debug.LogWarning(transform.name + ": LoadSkillRange", gameObject);
    }

    private void Update()
    {
        if (InputManager.Instance.GetKey(KeybindingActions.SlashBlast) && this._skillInfo.CurrentSkillLevel > 0 
            && !PlayerSkills.PlayerCtrl.PlayerMovement.RopeClimb.IsClimping && !PlayerSkills.PlayerCtrl.PlayerStats.IsDead && !PlayerSkills.IsAttacking)
        {
            int MPCost = _skillInfo.SkillProfile.InfoActiveSkill[this._skillInfo.CurrentSkillLevel].MPCost;
            if (!PlayerSkills.PlayerCtrl.PlayerStats.ReduceMP(MPCost)) return;
            StartCoroutine(PerformAttack());
        }
    }

    private void FixedUpdate()
    {
        this.newPos = _skillRange.position;
        this.newPos.x = newPos.x + (float)(0.5f * PlayerSkills.PlayerCtrl.PlayerMovement.FacingDirection);
        this.damageAble();
    }

    private void damageAble()
    {
        Vector3 newPos = _skillRange.position;
        newPos.x += (float)(0.5f * PlayerSkills.PlayerCtrl.PlayerMovement.FacingDirection);
    }

    private IEnumerator PerformAttack()
    {
        PlayerSkills.SetIsAttacking(true);

        ActiveSkill activeSkill = this._skillInfo.SkillProfile.InfoActiveSkill[_skillInfo.CurrentSkillLevel];

        int DamageSkill = activeSkill.DamageSkill;
        int maxDamage = PlayerStats.Instance.MaxDamage * DamageSkill / 100;
        int minDamage = PlayerStats.Instance.MinDamage * DamageSkill / 100 * 90 / 100;
        int criticalRate = PlayerSkills.PlayerCtrl.PlayerStats.CriticalRate;

        Damaged(minDamage, maxDamage + 1, criticalRate);

        Transform transform = SkillEffectSpawner.Instance.Spawn(SkillEffectSpawner.slashblast, this.newPos, Quaternion.identity);
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        transform.gameObject.SetActive(true);

        yield return new WaitForSeconds(_speed / PlayerSkills.PlayerCtrl.PlayerStats.AtttackSpeed);

        PlayerSkills.SetIsAttacking(false);
    }

    private void Damaged(int minDamage, int maxDamage, int criticalRate)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(this.newPos, new Vector2(2f, 1.5f), 0f, this._whatIsDamageAble);
        List<Transform> monsterList = new List<Transform>();

        foreach (Collider2D collider in colliders)
        {
            monsterList.Add(collider.transform);
            Debug.Log(collider.transform.name);
        }

        if (monsterList.Count > 0) monsterList.Reverse();

        if (monsterList.Count > this._maxEnemyHit)
        {
            for (int i = 0; i < this._maxEnemyHit; i++)
            {
                Debug.Log(monsterList[i].name);
                MonsterDamageReceiver damageAble = monsterList[i].GetComponentInChildren<MonsterDamageReceiver>();
                if (damageAble == null) continue;
                damageAble.ReceiveDamage(this._maxHit, minDamage, maxDamage, criticalRate);
            }
        }

        if (monsterList.Count <= this._maxEnemyHit)
        {
            Debug.Log(monsterList.Count);
            foreach (Transform transform in monsterList)
            {
                Debug.Log(transform.name);
                MonsterDamageReceiver damageAble = transform.GetComponentInChildren<MonsterDamageReceiver>();
                if (damageAble == null) continue;
                damageAble.ReceiveDamage(this._maxHit, minDamage, maxDamage, criticalRate);
            }
        }
    }

    public void RaiseSkillLevel()
    {
        if (!PlayerSkills.ReduceSkillPoint()) return;
        this._skillInfo.RaiseSkillLevel();
        UISlashBlast.Instance.SetSkillLevel(this._skillInfo.CurrentSkillLevel);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(newPos, new Vector2(2f, 1.5f));
    }
}
