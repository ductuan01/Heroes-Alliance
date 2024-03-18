using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EquipPickupable : EquipAbstract
{
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
        this.sphereCollider.radius = this._equipCtrl.equipBaseInfo.equipProfile.radiusCollider;
        Debug.LogWarning(transform.name + ": LoadTrigger", gameObject);
    }
}
