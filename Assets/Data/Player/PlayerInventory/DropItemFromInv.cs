using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropItemFromInv : SecondMonoBehaviour, IDropHandler
{
    private static DropItemFromInv instance;
    public static DropItemFromInv Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (DropItemFromInv.instance != null) Debug.LogError("Only 1 DropItemFromInv allow to exist");
        DropItemFromInv.instance = this;
    }

    private EquipDragDrop _equipDragDrop;
    private UseDragDrop _useDragDrop;
    private EtcDragDrop _etcDragDrop;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        GameObject dropObj = eventData.pointerDrag;
        _equipDragDrop = dropObj.GetComponent<EquipDragDrop>();
        _useDragDrop = dropObj.GetComponent<UseDragDrop>();
        _etcDragDrop = dropObj.GetComponent<EtcDragDrop>();

        if (_equipDragDrop != null)
        {
            this.EquipDrop();
            return;
        }

        if (_useDragDrop != null)
        {
            UIInventoryCtrl.Instance.amountDropBox.ShowBox(_useDragDrop.useInfo.useInformation.Amount);
            return;
        }

        if (_etcDragDrop != null)
        {
            UIInventoryCtrl.Instance.amountDropBox.ShowBox(_etcDragDrop.etcInfo.etcInformation.Amount);
            return;
        }
    }

    private void EquipDrop()
    {
        EquipInformation equipInfo = PlayerInventory.Instance.InfoEquipDrop(_equipDragDrop.equipInfo.equipInformation);
        Transform transform = EquipSpawner.Instance.Spawn(equipInfo.equipProfile.equipmentName.ToString(), PlayerCtrl.Instance.transform.position, Quaternion.identity);
        transform.GetComponentInChildren<EquipBaseInfo>().SetValue(equipInfo);
        transform.gameObject.SetActive(true);
        InventoryManager.Instance.InventoryChange();
    }

    public void UseAndEtcDrop(int amount)
    {
        if(_useDragDrop != null)
        {
            UseInformation useInfo = PlayerInventory.Instance.InfoUseDrop(_useDragDrop.useInfo.useInformation, amount);
            Transform transform = UseSpawner.Instance.Spawn(useInfo.useProfile.useCode.ToString(), PlayerCtrl.Instance.transform.position, Quaternion.identity);
            transform.gameObject.SetActive(true);
            UseBaseInfo useBaseInfo = transform.GetComponentInChildren<UseBaseInfo>();
            if (useBaseInfo == null) return;
            useBaseInfo.SetValue(useInfo);
            UIInventoryCtrl.Instance.amountDropBox.HideBox();
            InventoryManager.Instance.InventoryChange();
            return;
        }
        
        if(_etcDragDrop != null)
        {
            EtcInformation etcInfo = PlayerInventory.Instance.InfoEtcDrop(_etcDragDrop.etcInfo.etcInformation, amount);
            Transform transform = EtcSpawner.Instance.Spawn(etcInfo.etcProfile.etcCode.ToString(), PlayerCtrl.Instance.transform.position, Quaternion.identity);
            transform.gameObject.SetActive(true);
            EtcBaseInfo etcBaseInfo = transform.GetComponentInChildren<EtcBaseInfo>();
            if (etcBaseInfo == null) return;
            etcBaseInfo.SetValue(etcInfo);
            UIInventoryCtrl.Instance.amountDropBox.HideBox();
            InventoryManager.Instance.InventoryChange();
            return;
        }
    }
}
