using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerEquipmentInventoryCtrl : SecondMonoBehaviour
{
    private static UIPlayerEquipmentInventoryCtrl _instance;
    public static UIPlayerEquipmentInventoryCtrl Instance => _instance;

    private bool isOpen = true;

    protected override void Awake()
    {
        base.Awake();
        if (UIPlayerEquipmentInventoryCtrl._instance != null) Debug.LogError("Only 1 UIPlayerEquipmentInventoryCtrl allow to exist");
        UIPlayerEquipmentInventoryCtrl._instance = this;

        this.Toggle();
    }

    public virtual void Toggle()
    {
        this.isOpen = !this.isOpen;
        if (this.isOpen) this.gameObject.SetActive(true);
        else this.gameObject.SetActive(false);
    }
}
