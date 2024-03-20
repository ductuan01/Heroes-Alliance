using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryCtrl : SecondMonoBehaviour
{
    private static UIInventoryCtrl instance;
    public static UIInventoryCtrl Instance => instance;

    protected bool isOpen = true;
    protected bool isEquip = true;
    protected bool isUse = false;
    protected bool isEtc = false;

    [SerializeField] protected List<EquipInformation> _equipmentInventory;
    public List<EquipInformation> equipmentInventory => _equipmentInventory;

    [SerializeField] protected List<UseInformation> useInventory;
    public List<UseInformation> UseInventory => useInventory;

    [SerializeField] protected List<EtcInformation> etcInventory;
    public List<EtcInformation> EtcInventory => etcInventory;

    [SerializeField] protected int NTD;

    [SerializeField] protected UINTD uiNTDCtrl;

    [SerializeField] protected UIInfoCtrl _uiInfoCtrl;
    public UIInfoCtrl uiInfoCtrl => _uiInfoCtrl;

    [SerializeField] protected EquipSlotsCtrl _equipSlotsCtrl;
    [SerializeField] protected UseSlotsCtrl _useSlotsCtrl;
    [SerializeField] protected EtcSlotsCtrl _etcSlotsCtrl;

    [SerializeField] private AmountDropBox _amountDropBox;
    public AmountDropBox amountDropBox => _amountDropBox;
    protected override void Awake()
    {
        base.Awake();
        if (UIInventoryCtrl.instance != null) Debug.LogError("Only 1 UIInventoryCtrl allow to exist");
        UIInventoryCtrl.instance = this;
    }

    protected override void Start()
    {
        base.Start();
        this.Toggle();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadUINTDCtrl();

        this.LoadUIInfoCtrl();

        this.LoadInventoryManager();

        this.LoadEquipSlotsCtrl();
        this.LoadUseSlotsCtrl();
        this.LoadEtcSlotsCtrl();
    }

    private void LoadEquipSlotsCtrl()
    {
        if (_equipSlotsCtrl != null) return;
        this._equipSlotsCtrl = transform.Find("Scroll View").Find("Viewport").GetComponentInChildren<EquipSlotsCtrl>();
        Debug.LogWarning(transform.name + ": LoadEquipSlotsCtrl", gameObject);
    }

    private void LoadUseSlotsCtrl()
    {
        if (_useSlotsCtrl != null) return;
        this._useSlotsCtrl = transform.Find("Scroll View").Find("Viewport").GetComponentInChildren<UseSlotsCtrl>();
        Debug.LogWarning(transform.name + ": LoadUseSlotsCtrl", gameObject);
    }

    private void LoadEtcSlotsCtrl()
    {
        if (_etcSlotsCtrl != null) return;
        this._etcSlotsCtrl = transform.Find("Scroll View").Find("Viewport").GetComponentInChildren<EtcSlotsCtrl>();
        Debug.LogWarning(transform.name + ": LoadEtcSlotsCtrl", gameObject);
    }

    protected virtual void LoadUIInfoCtrl()
    {
        if (_uiInfoCtrl != null) return;
        this._uiInfoCtrl = transform.GetComponentInChildren<UIInfoCtrl>();
        Debug.Log(transform.name + ": LoadUIInfoCtrl", gameObject);
    }

    [SerializeField] private InventoryManager _inventoryManager;
    public InventoryManager inventoryManager => _inventoryManager;

    protected virtual void LoadUINTDCtrl()
    {
        if (uiNTDCtrl != null) return;
        this.uiNTDCtrl = transform.GetComponentInChildren<UINTD>();
        Debug.Log(transform.name + ": LoadUINTDCtrl", gameObject);
    }

    protected virtual void LoadInventoryManager()
    {
        if (_inventoryManager != null) return;
        this._inventoryManager = transform.GetComponentInChildren<InventoryManager>();
        Debug.Log(transform.name + ": LoadInventoryManager", gameObject);
    }

    protected override void OnEnable()
    {
        //PlayerInventory.instance.InventoryChange();
        _inventoryManager.OnInventoryChange += HandleEquipInvChange;
        _inventoryManager.OnInventoryChange += HandleUseInvChange;
        _inventoryManager.OnInventoryChange += HandleEtcInvChange;
        //_inventoryManager.OnNTDChange += HandleNTDInvChange;
        Debug.Log("Subscribe event");
    }

    protected override void OnDisable()
    {
        _inventoryManager.OnInventoryChange -= HandleEquipInvChange;
        _inventoryManager.OnInventoryChange -= HandleUseInvChange;
        _inventoryManager.OnInventoryChange -= HandleEtcInvChange;
        //_inventoryManager.OnNTDChange -= HandleNTDInvChange;
        Debug.Log("Unsubscribing event");
    }

    private void HandleEquipInvChange()
    {
        for (int i = 0; i < this._equipSlotsCtrl.equipSlots.Count; i++)
        {
            this._equipSlotsCtrl.equipSlots[i].equipDragDrop.SetUIEquip();
        }
    }
    private void HandleUseInvChange()
    {
        for (int i = 0; i < this._useSlotsCtrl.useSlots.Count; i++)
        {
            this._useSlotsCtrl.useSlots[i].useDragDrop.SetUIUse();
        }
    }
    private void HandleEtcInvChange()
    {
        for (int i = 0; i < this._etcSlotsCtrl.etcSlots.Count; i++)
        {
            this._etcSlotsCtrl.etcSlots[i].etcDragDrop.SetUIEtc();
        }
    }

    public virtual void LoadInventory()
    {
        this._equipmentInventory = PlayerCtrl.Instance.PlayerInventory.EquipInventory;
        this.useInventory = PlayerCtrl.Instance.PlayerInventory.UseInventory;
        this.etcInventory = PlayerCtrl.Instance.PlayerInventory.EtcInventory;
        //this.NTD = PlayerCtrl.instance.playerInventory.ntd;
    }

    public virtual void Toggle()
    {
        this.transform.SetParent(transform.parent.Find("ForArrangeFirst").transform);
        this.transform.SetParent(transform.parent.parent);

        this.uiNTDCtrl.HandleNTDChange(PlayerInventory.Instance.Ntd);
        this.HandleEquipInvChange();
        this.HandleUseInvChange();
        this.HandleEtcInvChange();


        this.isOpen = !this.isOpen;
        if (this.isOpen) gameObject.SetActive(true);
        else gameObject.SetActive(false);
        this.LoadInventory();

        

        if (this.isEquip && !this.isUse && !this.isEtc)
        {
            this.LoadEquipInv();
            return;
        }
        if (!this.isEquip && this.isUse && !this.isEtc)
        {
            this.LoadUseInv();
            return;
        }
        if (!this.isEquip && !this.isUse && this.isEtc)
        {
            this.LoadEtcInv();
            return;
        }
    }

    public virtual void LoadEquipInv()
    {
        this._equipSlotsCtrl.gameObject.SetActive(true);
        //this.isEquip = false;
        this.transform.Find("BtnTypeItem").Find("Equip").GetComponent<Image>().color = new Color(255 / 255f, 170 / 255f, 0 / 255f);
        this.transform.Find("BtnTypeItem").Find("Use").GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
        this.transform.Find("BtnTypeItem").Find("Etc").GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
        this.transform.Find("Scroll View").GetComponent<ScrollRect>().content = this._equipSlotsCtrl.GetComponent<RectTransform>();
        this.HandleEquipInvChange();
        this._useSlotsCtrl.gameObject.SetActive(false);
        this.isUse = true;
        this._etcSlotsCtrl.gameObject.SetActive(false);
        this.isEtc = true;
    }

    public virtual void LoadUseInv()
    {
        this._useSlotsCtrl.gameObject.SetActive(true);
        //this.isEtc = false;
        this.transform.Find("BtnTypeItem").Find("Equip").GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
        this.transform.Find("BtnTypeItem").Find("Use").GetComponent<Image>().color = new Color(255 / 255f, 170 / 255f, 0 / 255f);
        this.transform.Find("BtnTypeItem").Find("Etc").GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
        this.transform.Find("Scroll View").GetComponent<ScrollRect>().content = this._useSlotsCtrl.GetComponent<RectTransform>();
        HandleUseInvChange();
        this._equipSlotsCtrl.gameObject.SetActive(false);
        this.isEquip = true;
        this._etcSlotsCtrl.gameObject.SetActive(false);
        this.isEtc = true;
    }

    public virtual void LoadEtcInv()
    {
        this._etcSlotsCtrl.gameObject.SetActive(true);
        //this.isEtc = false;
        this.transform.Find("BtnTypeItem").Find("Equip").GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
        this.transform.Find("BtnTypeItem").Find("Use").GetComponent<Image>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
        this.transform.Find("BtnTypeItem").Find("Etc").GetComponent<Image>().color = new Color(255 / 255f, 170 / 255f, 0 / 255f);
        this.transform.Find("Scroll View").GetComponent<ScrollRect>().content = this._etcSlotsCtrl.GetComponent<RectTransform>();
        HandleEtcInvChange();
        this._equipSlotsCtrl.gameObject.SetActive(false);
        this.isEquip = true;
        this._useSlotsCtrl.gameObject.SetActive(false);
        this.isUse = true;
    }
}

