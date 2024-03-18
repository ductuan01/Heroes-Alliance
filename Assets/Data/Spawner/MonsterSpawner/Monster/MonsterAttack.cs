using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonsterAbstract
{
    [SerializeField] private Transform playerCheck;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private bool _isPlayer;

    [SerializeField] private bool _isAttacking;
    public bool IsAttacking => _isAttacking;

    [SerializeField] private float _speed = 1f;

    private const float _attackSpeed = 1f;

    public List<Transform> _players;

    private void FixedUpdate()
    {
        this.CheckPlayer();
        if (this._isPlayer && !PlayerStats.Instance.IsDead)
        {
            StartCoroutine(PerformAttack());
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this._isAttacking = false;
    }

    private void CheckPlayer()
    {
        this._isPlayer = Physics2D.OverlapBox(playerCheck.position, new Vector2(2f, 1.5f), 0f, whatIsPlayer);
    }

    private IEnumerator PerformAttack()
    {
        if (!_isAttacking && !MonsterCtrl.MonsterStats.isDead)
        {
            _isAttacking = true;
            //monsterCtrl.monsterAnim.Anim.SetTrigger("Attack");
            MonsterCtrl.MonsterMovement.SetChaseActive();

            yield return new WaitForSeconds(_attackSpeed * _speed);

            _isAttacking = false;
            this.Focus();
            this.Damaged();
        }
    }

    private void Focus()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(playerCheck.position, new Vector2(2f, 1.5f), 0f, whatIsPlayer);
        if (_players != null) _players.Clear();
        if (colliders.Length != 0) this._players.Add(colliders[0].transform);
    }

    private void Damaged()
    {
        if (_players.Count <= 0) return;
        PlayerDamageReceiver playerDamageReceiver = _players[0].GetComponentInChildren<PlayerDamageReceiver>();
        if (playerDamageReceiver == null) return;
        playerDamageReceiver.ReceiverDamage(1, MonsterCtrl.MonsterStats.Damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(playerCheck.position, new Vector3(2f, 1.5f, 0f));
    }
}
