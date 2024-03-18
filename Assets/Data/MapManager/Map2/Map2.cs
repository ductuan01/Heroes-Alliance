using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map2 : SecondMonoBehaviour
{
    [Header("Item Pickupable")]
    [SerializeField] protected SphereCollider sphereCollider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTrigger();
    }

    protected virtual void LoadTrigger()
    {
        if (sphereCollider != null) return;
        this.sphereCollider = transform.GetComponent<SphereCollider>();
        this.sphereCollider.isTrigger = true;
        Debug.LogWarning(transform.name + ": LoadTrigger", gameObject);
    }
}