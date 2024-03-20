using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamageReceiver : MonsterAbstract
{
    private bool _isHit = false;
    private Vector3 _hitStartPosition;
    private Vector3 _hitEndPosition;
    private float _hitDuration = 0.50f;
    private float _hitTimer = 0.0f;

    private void FixedUpdate()
    {
        if (_isHit)
        {
            _hitTimer += Time.deltaTime;

            float t = Mathf.Clamp01(_hitTimer / _hitDuration);

            transform.parent.position = Vector3.Lerp(_hitStartPosition, _hitEndPosition, t);

            if (t >= 0.5f)
            {
                _isHit = false;
            }
        }
    }

    public void ReceiveDamage(int hit, int minDamage, int maxDamage, int criticalRate)
    {
        Vector3 posPopup = transform.position;
        if (MonsterCtrl.MonsterStats.isDead) return;
        Hit();
        List<(int damage, bool isCrit)> damageList = new List<(int, bool)>();
        for (int i = 0; i < hit; i++)
        {
            bool isCriticalHit = Random.Range(0, 100) < criticalRate;
            int Damage = Random.Range(minDamage, maxDamage + 1);
            if (isCriticalHit)
            {
                Damage *= (1 + 100/100);  // is crit raise dame = 200%
            }
            damageList.Add((Damage, isCriticalHit));
            
        }
        StartCoroutine(SpawnDamagePopups(damageList, posPopup));
        StartCoroutine(SpawnImpact(hit, posPopup));
        foreach (var (damage, isCrit) in damageList)
        {
            if (!this.MonsterCtrl.MonsterStats.ReduceHP(damage)) return;
        }
    }

    private void Hit()
    {
        _isHit = true;
        this.MonsterCtrl.MonsterMovement.SetChaseActive();

        _hitStartPosition = transform.position;
        if(PlayerCtrl.Instance.transform.position.x > transform.position.x)
        {
            _hitEndPosition = transform.position - new Vector3(0.5f, 0.0f, 0.0f);
        }
        if (PlayerCtrl.Instance.transform.position.x < transform.position.x)
        {
            _hitEndPosition = transform.position + new Vector3(0.5f, 0.0f, 0.0f);
        }
        _hitTimer = 0.0f;
    }

    private IEnumerator SpawnDamagePopups(List<(int Damage, bool isCrit)> listDamage, Vector3 pos)
    {
        foreach (var (Damage, isCrit) in listDamage)
        {
            Vector3 newPos = pos;
            newPos.y += 0.5f;
            Transform transform = DamagePopupSpawner.Instance.Spawn(DamagePopupSpawner.damagePopup, newPos, Quaternion.identity);
            transform.localScale = new Vector3(1f, 1f, 1f);
            transform.GetComponent<DamagePopupCtrl>().Setup(Damage, isCrit);
            transform.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator SpawnImpact(int hit, Vector3 pos)
    {
        for (int i = 0; i < hit; i++)
        {
            Vector3 newPos = pos + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
            Transform transform = ImpactSpawner.Instance.Spawn(ImpactSpawner.impact_1, newPos, Quaternion.identity);
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            transform.GetComponent<ImpactCtrl>().Setup(0.5f);
            transform.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }
}