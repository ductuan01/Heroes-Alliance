using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyBindingsCtrl : SecondMonoBehaviour
{
    private static KeyBindingsCtrl _instance;
    public static KeyBindingsCtrl Instance => _instance;

    [SerializeField] private List<KeySlot> _keySlots;
    public List<KeySlot> keySlots => _keySlots;

    protected override void Awake()
    {
        if (KeyBindingsCtrl._instance != null) Debug.LogError("Only 1 KeyBindingsCtrl allow to exist");
        KeyBindingsCtrl._instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadKeySlots();
    }

    protected virtual void LoadKeySlots()
    {
        if (this._keySlots.Count > 0) return;
        foreach(Transform KeysBindings in this.transform)
        {
            foreach (Transform keyBinding in KeysBindings)
            {
                if (keyBinding.GetComponent<KeySlot>() != null) this._keySlots.Add(keyBinding.GetComponent<KeySlot>());
            }
        }
        Debug.LogWarning(transform.name + "LoadKeySlots", gameObject);
    }
}
