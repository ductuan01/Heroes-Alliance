using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIUseInfoCtrl : SecondMonoBehaviour
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
        this.itemName = transform.Find("UseName").GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadItemNameText", gameObject);
    }

    protected virtual void LoadItemImage()
    {
        if (this.itemImage != null) return;
        this.itemImage = transform.Find("BackGround").Find("UseImage").GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadItemImage", gameObject);
    }

    protected virtual void LoadItemDescription()
    {
        if (this.itemDescription != null) return;
        this.itemDescription = transform.Find("UseDescription").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadItemDescription", gameObject);
    }

    public virtual void SetItemName(UseInformation useInformation)
    {
        if (useInformation == null) return;
        string nameItem = useInformation.useProfile.useName;
        this.itemName.SetText(nameItem);
    }

    public virtual void SetItemImage(UseInformation useInformation)
    {
        if (useInformation == null) return;
        this.itemImage.sprite = useInformation.useProfile.useSprite;
    }
    public virtual void SetItemDescription(UseInformation useInformation)
    {
        if (useInformation == null) return;
        this.itemDescription.SetText(useInformation.useProfile.useDescription);
    }
}
