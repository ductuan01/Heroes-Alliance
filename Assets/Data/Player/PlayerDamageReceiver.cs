using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageReceiver : PlayerAbstract
{
    public void ReceiverDamage(int hit, int damage)
    {
        PlayerCtrl.PlayerStats.ReduceHP(damage);
        //StartCoroutine(SpawnDamagePopups(hit, transform.position, damage, false));
        StartCoroutine(SpawnImpact(hit, transform.position));
        PlayerCtrl.PlayerAnimation.BlinkBlink();
    }
/*    private IEnumerator SpawnDamagePopups(int hit, Vector3 pos, int damage, bool isCriticalHit)
    {
        for (int i = 0; i < hit; i++)
        {
            Vector3 newPos = pos;
            newPos.y += 0.5f;
            Transform transform = DamagePopupSpawner.Instance.Spawn(DamagePopupSpawner.damagePopup, newPos, Quaternion.identity);
            transform.localScale = new Vector3(1f, 1f, 1f);
            transform.GetComponent<DamagePopupCtrl>().Setup(damage, isCriticalHit);
            transform.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }*/

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
