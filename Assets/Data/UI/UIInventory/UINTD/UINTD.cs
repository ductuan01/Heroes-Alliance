using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINTD : UIInventoryAbstract
{
    [Header("UI NTD")]
    [SerializeField] private TextMeshProUGUI amountNTD;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTextMeshProUGUI();
    }
    protected virtual void LoadTextMeshProUGUI()
    {
        if (amountNTD != null) return;
        this.amountNTD = transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadTextMeshProUGUI", gameObject);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this.uiInventoryCtrl.inventoryManager.OnNTDChange += HandleNTDChange;
    }

    protected override void OnDisable()
    {
        base.OnEnable();
        this.uiInventoryCtrl.inventoryManager.OnNTDChange -= HandleNTDChange;
    }

    public virtual void HandleNTDChange(int ntd)
    {
        this.amountNTD.SetText(ntd.ToString());
    }
}
