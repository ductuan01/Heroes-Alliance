using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryAbstract : SecondMonoBehaviour
{
    [Header("Player Inventory Abstract")]
    [SerializeField] protected PlayerInventory playerInventory;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerInventory();
    }

    protected virtual void LoadPlayerInventory()
    {
        if (this.playerInventory != null) return;
        this.playerInventory = transform.parent.GetComponent<PlayerInventory>();
        Debug.LogWarning(transform.name + ": LoadPlayerInventory", gameObject);
    }
}
