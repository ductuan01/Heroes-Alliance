using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class EquipCtrl : SecondMonoBehaviour
{
    [SerializeField] private EquipModel _equipModel;
    public EquipModel equipModel => _equipModel;

    [SerializeField] private EquipBaseInfo _equipBaseInfo;
    public EquipBaseInfo equipBaseInfo => _equipBaseInfo;

    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private CircleCollider2D _cc;

/*
    [SerializeField] protected EquipmentDespawn equipmentDespawn;
    public EquipmentDespawn EquipmentDespawn => equipmentDespawn;

    [SerializeField] protected EquipInformation equipmentInformation;
    public EquipInformation EquipmentInformation => equipmentInformation;*/

    List<string> classType = new List<string> { "Warrior", "Magician", "Bowman", "Thief", "Pirate" };

/*    protected override void OnEnable()
    {
        base.OnEnable();
        this.ResetItem();
    }*/

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
        this.LoadEquipBaseInfo();
        this.LoadRigidbody2D();
        this.LoadCircleCollider2D();
        //this.LoadEquipmentDespawn();
        //this.LoadItemInventory();
        //this.LoadEquipmentInventory();
    }
    private void LoadModel()
    {
        if (this._equipModel != null) return;
        this._equipModel = transform.GetComponentInChildren<EquipModel>();
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }
    protected virtual void LoadEquipBaseInfo()
    {
        if (this._equipBaseInfo != null) return;
        this._equipBaseInfo = transform.GetComponentInChildren<EquipBaseInfo>();
        Debug.LogWarning(transform.name + ": LoadEquipBaseInfo", gameObject);
    }
    private void LoadRigidbody2D()
    {
        if (this._rb != null) return;
        this._rb = transform.GetComponent<Rigidbody2D>();
        this._rb.freezeRotation = true;
        Debug.LogWarning(transform.name + ": LoadCircleCollider2D", gameObject);
    }

    private void LoadCircleCollider2D()
    {
        if (this._cc != null) return;
        this._cc = transform.GetComponent<CircleCollider2D>();
        this._cc.radius = this._equipBaseInfo.equipProfile.radiusCollider;
        Debug.LogWarning(transform.name + ": LoadCircleCollider2D", gameObject);
    }
    /*    protected virtual void LoadModel()
        {
            if (this.model != null) return;
            this.model = transform.Find("Model");
            Debug.LogWarning(transform.name + ": LoadModel", gameObject);
        }*/

/*    protected virtual void LoadEquipmentDespawn()
    {
        if (this.equipmentDespawn != null) return;
        this.equipmentDespawn = transform.GetComponentInChildren<EquipmentDespawn>();
        Debug.LogWarning(transform.name + ": LoadEquipmentDespawn", gameObject);
    }

    protected virtual void LoadEquipmentInventory()
    {
        foreach (string classPlayer in classType)
        {
            string resPath = "ItemProfiles/Equipment/" + classPlayer + "/" + transform.name;
            //Debug.Log(resPath);
            EquipmentProfileSO equipmentProfile = Resources.Load<EquipmentProfileSO>(resPath);
            this.equipmentInformation.equipProfile = equipmentProfile;
            //this.ResetItem();
            if (equipmentProfile != null) break;
        }
    }

    protected virtual void ResetItem()
    {
        this.equipmentInformation.upgradeLevel = 0;
    }*/


/*    public virtual void SetItemInventory(ItemInventory itemInventory)
    {
        this.itemInventory = itemInventory;
    }*/
}
