using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class EtcCtrl : SecondMonoBehaviour
{
    [SerializeField] private EtcModel _etcModel;
    public EtcModel etcModel => _etcModel;

    [SerializeField] private EtcBaseInfo _etcBaseInfo;
    public EtcBaseInfo etcBaseInfo => _etcBaseInfo;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private CircleCollider2D _cc;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
        //this.LoadUseProfileSO();
        this.LoadEtcBaseInfo();
        this.LoadRigidbody2D();
        this.LoadCircleCollider2D();
        //this.LoadEquipmentInventory();
    }
    private void LoadModel()
    {
        if (this._etcModel != null) return;
        this._etcModel = transform.GetComponentInChildren<EtcModel>();
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }
    protected virtual void LoadEtcBaseInfo()
    {
        if (this._etcBaseInfo != null) return;
        this._etcBaseInfo = transform.GetComponentInChildren<EtcBaseInfo>();
        Debug.LogWarning(transform.name + ": LoadEtcBaseInfo", gameObject);
    }
    private void LoadRigidbody2D()
    {
        if (this._rb != null) return;
        this._rb = transform.GetComponent<Rigidbody2D>();
        this._rb.freezeRotation = true;
        Debug.LogWarning(transform.name + ": LoadCircleCollider2D", gameObject);
    }

    private void LoadCircleCollider2D()
    {
        if (this._cc != null) return;
        this._cc = transform.GetComponent<CircleCollider2D>();
        this._cc.radius = this._etcBaseInfo.etcProfile.radiusCollider;
        Debug.LogWarning(transform.name + ": LoadCircleCollider2D", gameObject);
    }

/*    [SerializeField] protected EtcInformation etcInformation;
    public EtcInformation EtcInformation => etcInformation;*/

/*    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEquipmentInventory();
    }*/

/*    protected virtual void LoadEquipmentInventory()
    {
        string resPath = "ItemProfiles/Etc/" + transform.name;
        Debug.Log(resPath);
        EtcProfileSO etfProfile = Resources.Load<EtcProfileSO>(resPath);
        this.etcInformation.etcProfile = etfProfile;
    }
*/

/*    public virtual void SetItemInventory(ItemInventory itemInventory)
    {
        this.itemInventory = itemInventory;
    }*/
}
