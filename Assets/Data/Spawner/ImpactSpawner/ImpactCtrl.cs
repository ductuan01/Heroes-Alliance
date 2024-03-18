using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactCtrl : MonoBehaviour
{
    private float despwanTimer = 0.5f;// = 0.5f;
    public void Setup(float despwanTimer)
    {
        this.despwanTimer = despwanTimer;
    }

    private void OnEnable()
    {
        this.despwanTimer = 0.5f;
    }

    private void Update()
    {
        despwanTimer -= Time.deltaTime;
        if (despwanTimer < 0)
        {
            ImpactSpawner.Instance.Despawn(transform);
        }
    }
}
