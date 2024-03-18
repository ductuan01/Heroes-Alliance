using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIEquipInfoCtrl : SecondMonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _Type;
    [SerializeField] private TextMeshProUGUI _STR;
    [SerializeField] private TextMeshProUGUI _DEX;
    [SerializeField] private TextMeshProUGUI _LUK;
    [SerializeField] private TextMeshProUGUI _INT;
    [SerializeField] private TextMeshProUGUI _MaxHP;
    [SerializeField] private TextMeshProUGUI _MaxMP;
    [SerializeField] private TextMeshProUGUI _WeaponATT;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemNameText();
        this.LoadItemImage();
        this.LoadItemInfo();
    }

    protected virtual void LoadItemNameText()
    {
        if (this._itemName != null) return;
        this._itemName = transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadItemNameText", gameObject);
    }

    protected virtual void LoadItemImage()
    {
        if (this._itemImage != null) return;
        this._itemImage = transform.Find("BackGround").Find("Image").GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadItemImage", gameObject);
    }

    private void LoadItemInfo()
    {
        if (this._Type != null || this._STR != null || this._DEX != null || 
            this._LUK != null || this._INT != null || this._MaxHP != null || 
            this._MaxMP != null || this._WeaponATT != null) return;
        this._Type = transform.Find("Type").GetComponent<TextMeshProUGUI>();
        this._STR = transform.Find("STR").GetComponent<TextMeshProUGUI>();
        this._DEX = transform.Find("DEX").GetComponent<TextMeshProUGUI>();
        this._INT = transform.Find("INT").GetComponent<TextMeshProUGUI>();
        this._LUK = transform.Find("LUK").GetComponent<TextMeshProUGUI>();
        this._MaxHP = transform.Find("MaxHP").GetComponent<TextMeshProUGUI>();
        this._MaxMP = transform.Find("MaxMP").GetComponent<TextMeshProUGUI>();
        this._WeaponATT = transform.Find("WeaponATT").GetComponent<TextMeshProUGUI>();
    }

    public virtual void SetItemName(EquipInformation equipmentInformation)
    {
        if (equipmentInformation == null) return;
        string nameItem = equipmentInformation.equipProfile.equipmentName.ToString();
        int upgradeLevel = equipmentInformation.upgradeLevel;
        if(upgradeLevel == 0) this._itemName.SetText(nameItem);
        else this._itemName.SetText(nameItem + " + " + upgradeLevel);
    }

    public virtual void SetItemImage(EquipInformation equipmentInformation)
    {
        if (equipmentInformation == null) return; 
        this._itemImage.sprite = equipmentInformation.equipProfile.equipSprite;
    }
    public virtual void SetItemInfo(EquipInformation equipmentInformation)
    {
        if (equipmentInformation == null) return;
        this._Type.SetText("Type: +" + equipmentInformation.equipProfile.equipmentType);
        this._STR.SetText("STR: +" + equipmentInformation.equipProfile.STR);
        this._DEX.SetText("DEX: +" + equipmentInformation.equipProfile.DEX);
        this._INT.SetText("INT: +" + equipmentInformation.equipProfile.INT);
        this._LUK.SetText("LUK: +" + equipmentInformation.equipProfile.LUK);
        this._MaxHP.SetText("Max HP: +" + equipmentInformation.equipProfile.MaxHP);
        this._MaxMP.SetText("Max MP: +" + equipmentInformation.equipProfile.MaxMP);
        this._WeaponATT.SetText("Weapon Attack: +" + equipmentInformation.equipProfile.AttPower);
    }
}
