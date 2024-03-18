using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsContainKeyBindingsCtrl : SecondMonoBehaviour
{
    private static SlotsContainKeyBindingsCtrl _instance;
    public static SlotsContainKeyBindingsCtrl Instance => _instance;

    [SerializeField] private List<KeySlot> _keySlots;
    public List<KeySlot> keySlots => _keySlots;

    protected override void Awake()
    {
        if (SlotsContainKeyBindingsCtrl._instance != null) Debug.LogError("Only 1 SlotsContainKeyBindingsCtrl allow to exist");
        SlotsContainKeyBindingsCtrl._instance = this;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadKeySlots();
    }

    private void LoadKeySlots()
    {
        if (this._keySlots.Count > 0) return;

        foreach (Transform keyBinding in this.transform.Find("KeySlots"))
        {
            if (keyBinding.GetComponent<KeySlot>() != null) this._keySlots.Add(keyBinding.GetComponent<KeySlot>());
        }
        foreach (Transform keyBinding in this.transform.Find("KeyItems"))
        {
            if (keyBinding.GetComponent<KeySlot>() != null)
            {
                this._keySlots.Add(keyBinding.GetComponent<KeySlot>());
                keyBinding.gameObject.SetActive(false);
            }
        }
        foreach (Transform keyBinding in this.transform.Find("KeySkills"))
        {
            if (keyBinding.GetComponent<KeySlot>() != null)
            {
                this._keySlots.Add(keyBinding.GetComponent<KeySlot>());
                keyBinding.gameObject.SetActive(false);
            }
        }
        Debug.LogWarning(transform.name + "LoadKeySlots", gameObject);
    }
}
