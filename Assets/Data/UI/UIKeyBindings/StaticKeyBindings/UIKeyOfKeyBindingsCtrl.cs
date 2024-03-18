using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIKeyOfKeyBindingsCtrl : SecondMonoBehaviour
{
    private static UIKeyOfKeyBindingsCtrl _instance;
    public static UIKeyOfKeyBindingsCtrl Instance => _instance;

    [SerializeField] private List<KeySlot> _keySlots;
    public List<KeySlot> keySlots => _keySlots;

    protected override void Awake()
    {
        base.Awake();
        if (UIKeyOfKeyBindingsCtrl._instance != null) Debug.LogError("Only 1 UIKeyOfKeyBindingsCtrl allow to exist");
        UIKeyOfKeyBindingsCtrl._instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadKeySlots();
    }

    private void LoadKeySlots()
    {
        if (this._keySlots.Count > 0) return;

        foreach(Transform keySlot in transform.Find("KeySlots"))
        {
            if (keySlot.GetComponent<KeySlot>() != null) this._keySlots.Add(keySlot.GetComponent<KeySlot>());
        }
        Debug.Log(transform.name + "LoadKeySlots", gameObject);
    }
}