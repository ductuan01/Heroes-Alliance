using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyBindingsCtrl : Spawner
{
    private static UIKeyBindingsCtrl _instance;
    public static UIKeyBindingsCtrl Instance => _instance;

    [SerializeField] private KeyBindingsMenuCtrl _keyBindingsMenuCtrl;
    [SerializeField] private KeyBindingsCtrl _keyBindingsCtrl;
    [SerializeField] private SlotsContainKeyBindingsCtrl _slotsContainKey;
    [SerializeField] private UIKeyOfKeyBindingsCtrl _uIKeyOfKeyBindings;

    [SerializeField] private InventoryManager _inventoryManager;

    protected bool isOpen = false;

    protected override void Awake()
    {
        base.Awake();
        if (UIKeyBindingsCtrl._instance != null) Debug.LogError("Only 1 UIKeyBindingsCtrl allow to exist");
        UIKeyBindingsCtrl._instance = this;
    }

    protected override void Start()
    {
        base.Start();
        this._keyBindingsMenuCtrl.gameObject.SetActive(false);
        this.LoadKeyBindingsSO(this._keyBindingsCtrl.keySlots, InputManager.Instance.KeyBindings);
    }
    protected override void OnEnable()
    {
        _inventoryManager.OnUseAmountChange += FixUseAmount;
        Debug.Log("Subscribe event");
    }

    protected override void OnDisable()
    {
        _inventoryManager.OnUseAmountChange -= FixUseAmount;
        Debug.Log("Unsubscribing event");
    }

    public virtual void KeyBindingsToggle()
    {
        this.isOpen = !this.isOpen;
        if (this.isOpen) this.OpenKeyBindings();
        else this.CloseKeyBindings();
    }

    public virtual void CloseKeyBindings()
    {
        _keyBindingsMenuCtrl.gameObject.SetActive(false);
    }
    public virtual void OpenKeyBindings()
    {
        _keyBindingsMenuCtrl.gameObject.SetActive(true);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPrefabs();
    }

    private void LoadKeyBindingsMenuCtrl()
    {
        if (this._keyBindingsMenuCtrl != null) return;
        this._keyBindingsMenuCtrl = transform.GetComponentInChildren<KeyBindingsMenuCtrl>();
        Debug.LogWarning(transform.name + "LoadKeyBindingsMenuCtrl", gameObject);
    }

    private void LoadKeyBindingsCtrl()
    {
        if (this._keyBindingsCtrl != null) return;
        this._keyBindingsCtrl = transform.Find("KeyBindingsMenu").GetComponentInChildren<KeyBindingsCtrl>();
        Debug.LogWarning(transform.name + "LoadKeyBindingsCtrl", gameObject);
    }

    private void LoadSlotsContainKeyBindingsCtrl()
    {
        if (this._slotsContainKey != null) return;
        this._slotsContainKey = transform.Find("KeyBindingsMenu").GetComponentInChildren<SlotsContainKeyBindingsCtrl>();
        Debug.LogWarning(transform.name + "LoadSlotsContainKeyBindingsCtrl", gameObject);
    }

    private void LoadUIKeyOfKeyBindingsCtrl()
    {
        if (this._uIKeyOfKeyBindings != null) return;
        this._uIKeyOfKeyBindings = transform.GetComponentInChildren<UIKeyOfKeyBindingsCtrl>();
        Debug.LogWarning(transform.name + "LoadUIKeyOfKeyBindingsCtrl", gameObject);
    }
    private void LoadInventoryManager()
    {
        if (this._inventoryManager != null) return;
        this._inventoryManager = GameObject.Find("UIInventory").GetComponentInChildren<InventoryManager>();
        Debug.LogWarning(transform.name + "LoadUIKeyOfKeyBindingsCtrl", gameObject);
    }

    protected override void LoadPrefabs()
    { 
        this.LoadKeyBindingsMenuCtrl();
        this.LoadKeyBindingsCtrl();
        this.LoadSlotsContainKeyBindingsCtrl();
        this.LoadUIKeyOfKeyBindingsCtrl();
        this.LoadInventoryManager();
        if (this.prefabs.Count > 0) return;
        foreach (KeySlot keySlot in _slotsContainKey.keySlots)
        {
            KeyDragDrop dragKey = keySlot.GetComponentInChildren<KeyDragDrop>();
            if (dragKey == null) continue;
            prefabs.Add(dragKey.transform);
        }
    }

    public void SetKeyBindings()
    {
        InputManager.Instance.ClearKeyBindings();

        foreach (KeySlot keySlot in this._keyBindingsCtrl.keySlots)
        {
            KeyDragDrop dragKey = keySlot.GetComponentInChildren<KeyDragDrop>();
            if (dragKey != null)
            {
                KeyBindingCheck newKey = CreateKeyBindingByKeyCode(dragKey.transform);
                InputManager.Instance.AddKeyBindings(newKey);
            }
        }
    }

    private KeyBindingCheck CreateKeyBindingByKeyCode(Transform dragKey)
    {
        KeyBindingCheck newKey = new KeyBindingCheck();

        if (Enum.TryParse(dragKey.name, out KeybindingActions action))
        {
            if (action == KeybindingActions.NoAction) return null;
            newKey.keybindingActions = action;
        }

        KeyCode keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), dragKey.transform.parent.name);
        newKey.keyCode = keyCode;

        return newKey;
    }

    public void FixUseAmount(UseCode useCode)
    {
        Debug.Log("ahihi");
        foreach (KeySlot keySlot in this._keyBindingsCtrl.keySlots)
        {
            KeyDragDrop dragKey = keySlot.GetComponentInChildren<KeyDragDrop>();
            if (dragKey == null) continue;
            if (dragKey.name != useCode.ToString()) continue;

            TextMeshProUGUI totalAmount = dragKey.GetComponentInChildren<TextMeshProUGUI>();
            totalAmount.SetText(PlayerCtrl.Instance.PlayerInventory.GetTotalAmountOfUseItem(useCode).ToString());
            foreach (KeySlot keySlotStatic in UIKeyOfKeyBindingsCtrl.Instance.keySlots)
            {
                if (keySlotStatic.name == keySlot.name)
                {
                    KeyDragDrop dragKeyStatic = keySlotStatic.GetComponentInChildren<KeyDragDrop>();
                    if (dragKeyStatic == null) continue;
                    if (dragKeyStatic.name != useCode.ToString()) continue;

                    TextMeshProUGUI totalAmount2 = dragKeyStatic.GetComponentInChildren<TextMeshProUGUI>();
                    totalAmount2.SetText(PlayerCtrl.Instance.PlayerInventory.GetTotalAmountOfUseItem(useCode).ToString());
                }
            }
            return;
        }
    }

    public bool DeleteDuplicateKey(List<KeySlot> keySlots, string nameKey)
    {
        foreach (KeySlot keySlot in keySlots)
        {
            KeyDragDrop dragkey = keySlot.GetComponentInChildren<KeyDragDrop>();
            if (dragkey == null) continue;
            if (dragkey.name == nameKey)
            {
                this.poolObjs.Add(dragkey.transform);
                dragkey.gameObject.SetActive(false);
            }
        }
        return true;
    }

    private bool LoadKeyBindingsSO(List<KeySlot> keySlots, KeyBindings keyBindings)
    {
        foreach (KeyBindingCheck keyBindingCheck in keyBindings.keyBindingChecks)
        {
            foreach (Transform prefab in this.prefabs)
            {
                Transform keySlot;
                Transform keySlotStatic;
                //Transform keyBinding;
                Debug.Log(keyBindingCheck.keybindingActions.ToString());
                Debug.Log(prefab.transform.name);

                if (keyBindingCheck.keybindingActions.ToString() == prefab.transform.name)
                {
                    prefab.gameObject.SetActive(false);
                    // for Keybiding toggleable
                    Transform transform = UIKeyBindingsCtrl.Instance.Spawn(prefab.transform.name, new Vector3(0f, 0f, 0f), Quaternion.identity);
                    if (transform == null) return false;

                    keySlot = FindKeySlot(this._keyBindingsCtrl.keySlots, keyBindingCheck);
                    if (keySlot == null) continue;

                    transform.parent = keySlot.transform;
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    transform.gameObject.SetActive(true);

                    /// for UI keybinding static
                    keySlotStatic = FindKeySlot(this._uIKeyOfKeyBindings.keySlots, keyBindingCheck);
                    if (keySlotStatic == null) continue;

                    Transform transform2 = UIKeyBindingsCtrl.Instance.Spawn(prefab.transform.name, new Vector3(0f, 0f, 0f), Quaternion.identity);
                    if (transform2 == null) return false;

                    transform2.parent = keySlotStatic.transform;
                    transform2.localScale = new Vector3(1f, 1f, 1f);
                    transform2.gameObject.SetActive(true);

                    ///
                    string resPath = "ItemProfiles/Use/" + "Recovery" + "/" + keyBindingCheck.keybindingActions.ToString();
                    UseProfileSO useProfile = Resources.Load<UseProfileSO>(resPath);
                    if (useProfile == null) continue;

                    transform.GetComponentInChildren<TextMeshProUGUI>().SetText(PlayerInventory.Instance.GetTotalAmountOfUseItem(useProfile.useCode).ToString());
                    transform2.GetComponentInChildren<TextMeshProUGUI>().SetText(PlayerInventory.Instance.GetTotalAmountOfUseItem(useProfile.useCode).ToString());
                }
            }
        }
        return true;
    }

    private Transform FindKeySlot(List<KeySlot> KeySlots, KeyBindingCheck keyBindingCheck)
    {
        foreach (KeySlot keySlot in KeySlots)
        {
            if (keySlot.transform.name == keyBindingCheck.keyCode.ToString())
            {
                return keySlot.transform;
            }
        }
        return null;
    }

    public virtual Transform GetPrefabByName(string prefabName)
    {
        foreach (Transform prefab in this.prefabs)
        {
            if (prefab.name == prefabName) return prefab;
        }
        return null;
    }
}
