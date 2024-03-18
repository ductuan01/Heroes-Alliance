using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

public class GoIntoPortal : PlayerInventoryAbstract
{
    //[SerializeField] protected PlayerInventory playerInventory;
    [SerializeField] protected SphereCollider sphereCollider;
    [SerializeField] protected new Rigidbody rigidbody;
    [SerializeField] private bool GoIntoAllowed = false;

    [SerializeField] private Map2 map2;

    protected string sceneName = "SampleScene";

    private void Update()
    {
        if (GoIntoAllowed && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Into Map 2");
            SceneManager.LoadScene(this.sceneName);
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerInventory();
        this.LoadTrigger();
        this.LoadRigidbody();
    }

    /*    protected virtual void LoadPlayerInventory()
        {
            if (this.playerInventory != null) return;
            this.playerInventory = transform.GetComponentInParent<PlayerInventory>();
            Debug.LogWarning(transform.name + ": LoadPlayerInventory", gameObject);
        }*/

    protected virtual void LoadTrigger()
    {
        if (this.sphereCollider != null) return;
        this.sphereCollider = transform.GetComponent<SphereCollider>();
        this.sphereCollider.isTrigger = true;
        this.sphereCollider.radius = 0.5f;
        Debug.LogWarning(transform.name + ": LoadTrigger", gameObject);
    }
    protected virtual void LoadRigidbody()
    {
        if (this.rigidbody != null) return;
        this.rigidbody = transform.GetComponent<Rigidbody>();
        this.rigidbody.useGravity = false;
        this.rigidbody.isKinematic = true;
        Debug.LogWarning(transform.name + ": LoadRigidbody", gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        this.map2 = other.GetComponent<Map2>();
        if (this.map2 == null) return;
        this.GoIntoAllowed = true;
        Debug.Log("this is Map 2");
        Debug.Log("Go into allowed");

    }

    protected virtual void OnTriggerExit(Collider other)
    {
        this.map2 = other.GetComponent<Map2>();
        if (this.map2 == null) return;
        this.GoIntoAllowed = false;
        Debug.Log("this is Map 2");
        Debug.Log("Go into doesn't allowed");
    }
}
