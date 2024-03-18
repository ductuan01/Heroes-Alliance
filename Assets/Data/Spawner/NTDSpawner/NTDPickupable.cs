using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class NTDPickupable : NTDAbstract
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
        this.sphereCollider.radius = this.ntdCtrl.ntdProfile.radiusCollider;
        Debug.LogWarning(transform.name + ": LoadTrigger", gameObject);
    }

    /*    protected virtual void LoadPlayerInventory()
        {
            if (this.playerInventory != null) return;
            this.playerInventory = FindObjectOfType<PlayerCtrl>().GetComponentInChildren<PlayerInventory>();
            Debug.LogWarning(transform.name + ": LoadPlayerInventory", gameObject);
        }*/

    /*    public static ItemCode String2ItemCode(string itemName)
        {
            return (ItemCode)System.Enum.Parse(typeof(ItemCode), itemName);
        }

        public virtual ItemCode GetItemCode()
        {
            return ItemPickupable.String2ItemCode(transform.parent.name);
        }*/
}
