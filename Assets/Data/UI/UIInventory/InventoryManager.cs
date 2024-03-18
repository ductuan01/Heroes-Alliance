using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SecondMonoBehaviour
{
    public delegate void InventoryChangeHandler();
    public event InventoryChangeHandler OnInventoryChange;

    public delegate void UseAmountChangeHandler(UseCode useCode);
    public event UseAmountChangeHandler OnUseAmountChange;

    public delegate void NTDChangeHandler(int ntd);
    public event NTDChangeHandler OnNTDChange;

    private static InventoryManager _instance;
    public static InventoryManager Instance => _instance;

    protected override void Awake()
    {
        base.Awake();
        if (InventoryManager._instance != null) Debug.LogError("Only 1 InventoryManager allow to exist");
        InventoryManager._instance = this;
    }

    public void InventoryChange()
    {
        OnInventoryChange?.Invoke();
    }

    public void UseAmountChange(UseCode useCode)
    {
        OnUseAmountChange?.Invoke(useCode);
    }

    public void NTDChange(int ntd)
    {
        OnNTDChange?.Invoke(ntd);
    }
}
