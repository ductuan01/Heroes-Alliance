using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerAbstract
{
    private static PlayerAttack _instance;
    public static PlayerAttack Instance => _instance;

    [SerializeField] private Transform _attackRange;

    [SerializeField] private int _maxEnemyHit = 1;

    [SerializeField] private int _maxHit = 1;

    [SerializeField] private float _speed = 4f;

    private Vector3 newPos = new Vector3();

    [SerializeField] private LayerMask _whatIsDamageAble;

    protected override void Awake()
    {
        base.Awake();
        if (PlayerAttack._instance != null) Debug.LogError("Only 1 PlayerAttack allow to exist");
        PlayerAttack._instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAttackRange();
        this._whatIsDamageAble = LayerMask.GetMask("Enemy");
    }

    private void LoadAttackRange()
    {
        if (this._attackRange != null) return;
        this._attackRange = transform.Find("Range");
        Debug.LogWarning(transform.name + ": LoadAttackRange", gameObject);
    }

    private void Update()
    {
        if (InputManager.Instance.GetKey(KeybindingActions.Attack) && !PlayerCtrl.PlayerMovement.RopeClimb.IsClimping && !PlayerCtrl.PlayerStats.IsDead && !PlayerCtrl.PlayerSkills.IsAttacking)
        {
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack()
    {
        PlayerCtrl.PlayerSkills.SetIsAttacking(true);

        Damaged(PlayerCtrl.PlayerStats.MinDamage, PlayerCtrl.PlayerStats.MaxDamage + 1, PlayerCtrl.PlayerStats.CriticalRate);

        this.newPos = this.transform.position;
        this.newPos.x = newPos.x + (float)(0.6f * PlayerCtrl.PlayerMovement.FacingDirection);

        Transform transform = SkillEffectSpawner.Instance.Spawn(SkillEffectSpawner.attack, this.newPos, Quaternion.identity);
        transform.localScale = new Vector3(0.15f * PlayerCtrl.PlayerMovement.FacingDirection, 0.15f, 0.15f);
        transform.gameObject.SetActive(true);

        yield return new WaitForSeconds(_speed / PlayerCtrl.PlayerStats.AtttackSpeed);

        PlayerCtrl.PlayerSkills.SetIsAttacking(false);
    }

    private void Damaged(int minDamage, int maxDamage, int criticalRate)
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(this.newPos, new Vector2(1.4f, 1f), 0f, this._whatIsDamageAble);
        List<Transform> monsterList = new List<Transform>();

        foreach (Collider2D collider in colliders)
        {
            monsterList.Add(collider.transform);
        }

        if (monsterList.Count > 0) monsterList.Reverse();

        foreach (Transform transform in monsterList)
        {
            MonsterDamageReceiver damageAble = transform.GetComponentInChildren<MonsterDamageReceiver>();
            if (damageAble == null) continue;
            damageAble.ReceiveDamage(this._maxHit, minDamage, maxDamage, criticalRate);
            return;
        }

        /*        if (monsterList.Count > this._maxEnemyHit)
                {
                    for (int i = 0; i < this._maxEnemyHit; i++)
                    {
                        MonsterDamageReceiver damageAble = monsterList[i].GetComponentInChildren<MonsterDamageReceiver>();
                        if (damageAble == null) continue;
                        damageAble.ReceiveDamage(this._maxHit, minDamage, maxDamage, criticalRate);
                    }
                }*/

/*        if (monsterList.Count <= this._maxEnemyHit)
        {
            foreach (Transform transform in monsterList)
            {
                MonsterDamageReceiver damageAble = transform.GetComponentInChildren<MonsterDamageReceiver>();
                if (damageAble == null) continue;
                damageAble.ReceiveDamage(this._maxHit, minDamage, maxDamage, criticalRate);
            }
        }*/
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(newPos, new Vector2(1.4f, 1f));
    }
}
