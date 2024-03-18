using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class NTDCtrl : SecondMonoBehaviour
{
    [SerializeField] private NTDModel _ndtModel;
    public NTDModel ndtModel => _ndtModel;

    [SerializeField] private NTDProfileSO _ntdProfile;
    public NTDProfileSO ntdProfile => _ntdProfile;

    [SerializeField] private NTDInfo ntdInfo;
    public NTDInfo NTDInfo => ntdInfo;

    //[SerializeField] private Rigidbody2D _rb;
    [SerializeField] private CircleCollider2D _cc;

    public Sprite ntdSprite { get; set; }

    protected override void LoadComponents()
    {
        this.LoadModel();
        this.LoadNTDProfileSO();
        this.LoadNTDInfo();
        //this.LoadRigidbody2D();
        this.LoadCircleCollider2D();
    }

    private void LoadModel()
    {
        if (this._ndtModel != null) return;
        this._ndtModel = transform.GetComponentInChildren<NTDModel>();
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }

    protected virtual void LoadNTDProfileSO()
    {
        string resPath = "ItemProfiles/NTD/" + transform.name;
        this._ntdProfile = Resources.Load<NTDProfileSO>(resPath);
        Debug.LogWarning(transform.name + ": LoadNTDProfileSO", gameObject);
    }

    protected virtual void LoadNTDInfo()
    {
        if (this.ntdInfo != null) return;
        this.ntdInfo = transform.GetComponentInChildren<NTDInfo>();
        Debug.LogWarning(transform.name + ": LoadNTDInfo", gameObject);
    }

/*    private void LoadRigidbody2D()
    {
        if (this._rb != null) return;
        this._rb = transform.GetComponent<Rigidbody2D>();
        Debug.LogWarning(transform.name + ": LoadRigidbody2D", gameObject);
    }*/

    private void LoadCircleCollider2D()
    {
        if (this._cc != null) return;
        this._cc = transform.GetComponent<CircleCollider2D>();
        Debug.LogWarning(transform.name + ": LoadCircleCollider2D", gameObject);
    }
}
