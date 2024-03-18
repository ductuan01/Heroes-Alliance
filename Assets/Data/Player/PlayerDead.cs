using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : PlayerAbstract
{
    private readonly float _flyDuration = 1f;
    protected override void Start()
    {
        base.Start();
        transform.gameObject.SetActive(false);
    }

    public void Active()
    {
        PlayerCtrl.PlayerModel.gameObject.SetActive(false);
        transform.gameObject.SetActive(true);

        Vector3 pos = PlayerCtrl.transform.position + Vector3.up * 10;
        transform.position = pos;

        Vector3 targetPostion = PlayerCtrl.transform.position + Vector3.down * 0.1f;
        StartCoroutine(FlyTo(targetPostion));
    }

    IEnumerator FlyTo(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;

        while (elapsedTime < _flyDuration)
        {
            float t = elapsedTime / _flyDuration;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = PlayerCtrl.transform.position + Vector3.down * 0.1f;
    }
}
