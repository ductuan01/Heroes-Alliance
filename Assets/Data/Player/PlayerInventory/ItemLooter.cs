using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

public class ItemLooter : PlayerInventoryAbstract
{
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private new Rigidbody rigidbody;

    private EquipPickupable _equipPickupable;
    private EquipBaseInfo _equipInfo;

    private UsePickupable _usePickupable;
    private UseBaseInfo _useInfo;

    private EtcPickupable _etcPickupable;
    private EtcBaseInfo _etcInfo;

    private NTDPickupable _ntdPickupable;

    private void Update()
    {
        if(InputManager.Instance.GetKeyDown(KeybindingActions.PickUp))
        {
            PickNearestItem();
        }
    }

    private void PickNearestItem()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);

        Collider itemToPickup = null;

        float closestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.name == "NTDPickupable" || collider.name == "UsePickupable" || collider.name == "EquipPickupable" || collider.name == "EtcPickupable")
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    itemToPickup = collider;
                }
            }
        }

        if (itemToPickup != null)
        {
            if (NTDPickupable(itemToPickup)) return;
            if (EquipmentPickupable(itemToPickup)) return;
            if (UsePickupable(itemToPickup)) return;
            if (EtcPickupable(itemToPickup)) return;
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerInventory();
        this.LoadTrigger();
        this.LoadRigidbody();
    }

    private void LoadTrigger()
    {
        if (this.sphereCollider != null) return;
        this.sphereCollider = transform.GetComponent<SphereCollider>();
        this.sphereCollider.isTrigger = true;
        this.sphereCollider.radius = 0.5f;
        Debug.LogWarning(transform.name + ": LoadTrigger", gameObject);
    }
    private void LoadRigidbody()
    {
        if (this.rigidbody != null) return;
        this.rigidbody = transform.GetComponent<Rigidbody>();
        this.rigidbody.useGravity = false;
        this.rigidbody.isKinematic = true;
        Debug.LogWarning(transform.name + ": LoadRigidbody", gameObject);
    }

    private bool EquipmentPickupable(Collider itemToPickup)
    {
        this._equipPickupable = itemToPickup.GetComponent<EquipPickupable>();
        if (this._equipPickupable == null) return false;
        this._equipInfo = this._equipPickupable.equipCtrl.equipBaseInfo;
        if(!this.playerInventory.AddEquipToInv(this._equipInfo.equipInformation)) return false;
        EquipSpawner.Instance.Despawn(itemToPickup.transform.parent);
        return true;
    }

    private bool UsePickupable(Collider itemToPickup)
    {
        this._usePickupable = itemToPickup.GetComponent<UsePickupable>();
        if (this._usePickupable == null) return false;
        this._useInfo = this._usePickupable.useCtrl.useBaseInfo;
        if (!this.playerInventory.AddUseToInv(this._useInfo.useInformation)) return false;
        UseSpawner.Instance.Despawn(this._useInfo.transform.parent);
        return true;
    }

    private bool EtcPickupable(Collider itemToPickup)
    {
        this._etcPickupable = itemToPickup.GetComponent<EtcPickupable>();
        if (this._etcPickupable == null) return false;
        this._etcInfo = this._etcPickupable.etcCtrl.etcBaseInfo;
        if (!this.playerInventory.AddEtcToInv(this._etcInfo.etcInformation)) return false;
        EtcSpawner.Instance.Despawn(this._etcInfo.transform.parent);
        return true;
    }

    private bool NTDPickupable(Collider itemToPickup)
    {
        this._ntdPickupable = itemToPickup.GetComponent<NTDPickupable>();
        if (this._ntdPickupable == null) return false;
        int ntdAmount = this._ntdPickupable.ntdCtrl.NTDInfo.ntdAmount;
        if(!this.playerInventory.RaiseNTD(ntdAmount)) return false;
        NTDSpawner.Instance.Despawn(this._ntdPickupable.transform.parent);
        return true;
    }
}

