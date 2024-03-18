using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEtcInfoCtrl : SecondMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI itemName;
    [SerializeField] protected Image itemImage;
    [SerializeField] protected TextMeshProUGUI itemDescription;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemNameText();
        this.LoadItemImage();
        this.LoadItemDescription();
    }

    protected virtual void LoadItemNameText()
    {
        if (this.itemName != null) return;
        this.itemName = transform.Find("EtcName").GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadItemNameText", gameObject);
    }

    protected virtual void LoadItemImage()
    {
        if (this.itemImage != null) return;
        this.itemImage = transform.Find("EtcImage").GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadItemImage", gameObject);
    }

    protected virtual void LoadItemDescription()
    {
        if (this.itemDescription != null) return;
        this.itemDescription = transform.Find("EtcDescription").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadItemDescription", gameObject);
    }

    public virtual void SetItemName(EtcInformation etcInformation)
    {
        if (etcInformation == null) return;
        string nameItem = "";
        if (etcInformation.etcProfile.etcCode != EtcCode.NoCode) nameItem = etcInformation.etcProfile.etcCode.ToString();
        this.itemName.SetText(nameItem);
    }

    public virtual void SetItemImage(EtcInformation etcInformation)
    {
        if (etcInformation == null) return;
        this.itemImage.sprite = etcInformation.etcProfile.etcSprite;
    }
    public virtual void SetItemDescription(EtcInformation etcInformation)
    {
        if (etcInformation == null) return;
        this.itemDescription.SetText(etcInformation.etcProfile.etcDescription);
    }
}
