using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerEquipmentInventoryCtrl : SecondMonoBehaviour
{
    private static UIPlayerEquipmentInventoryCtrl _instance;
    public static UIPlayerEquipmentInventoryCtrl Instance => _instance;

    [SerializeField] private PlayerEquipInvSlotsCtrl _playerEquipInvSlots;
    public  PlayerEquipInvSlotsCtrl PlayerEquipInvSlots => _playerEquipInvSlots;


    private bool isOpen = true;

    protected override void Awake()
    {
        base.Awake();
        if (UIPlayerEquipmentInventoryCtrl._instance != null) Debug.LogError("Only 1 UIPlayerEquipmentInventoryCtrl allow to exist");
        UIPlayerEquipmentInventoryCtrl._instance = this;

        //this.Toggle();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerEquipInvSlotsCtrl();
    }

    private void LoadPlayerEquipInvSlotsCtrl()
    {
        if (this._playerEquipInvSlots != null) return;
        this._playerEquipInvSlots = transform.GetComponentInChildren<PlayerEquipInvSlotsCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerEquipInvSlotsCtrl", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.Toggle();
    }

    public virtual void Toggle()
    {
        this.transform.SetParent(transform.parent.Find("ForArrangeFirst").transform);
        this.transform.SetParent(transform.parent.parent);

        this.isOpen = !this.isOpen;
        if (this.isOpen) this.gameObject.SetActive(true);
        else this.gameObject.SetActive(false);
        _playerEquipInvSlots.LoadEquipImage();
    }
}
