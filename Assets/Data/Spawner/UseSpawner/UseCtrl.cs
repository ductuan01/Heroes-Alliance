using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class UseCtrl : SecondMonoBehaviour
{
    [SerializeField] private UseModel _useModel;
    public UseModel useModel => _useModel;

/*    [SerializeField] private UseProfileSO _useProfile;
    public UseProfileSO useProfile => _useProfile;*/

    [SerializeField] private UseBaseInfo _useBaseInfo;
    public UseBaseInfo useBaseInfo => _useBaseInfo;

    [SerializeField] private CircleCollider2D _cc;

/*    [SerializeField] protected UseInformation useInformation;
    public UseInformation UseInformation => useInformation;

    List<string> useTypes = new List<string> { "Recovery", "Scroll" };*/

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
        //this.LoadUseProfileSO();
        this.LoadUseBaseInfo();
        this.LoadCircleCollider2D();
        //this.LoadEquipmentInventory();
    }
    private void LoadModel()
    {
        if (this._useModel != null) return;
        this._useModel = transform.GetComponentInChildren<UseModel>();
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }
/*    protected virtual void LoadUseProfileSO()
    {
        if (this._useProfile != null) return;
        foreach (string useType in useTypes)
        {
            string resPath = "ItemProfiles/Use/" + useType + "/" + transform.name;
            this._useProfile = Resources.Load<UseProfileSO>(resPath);
            if (useProfile != null) break;
        }
        Debug.LogWarning(transform.name + ": LoadUseProfileSO", gameObject);
    }*/

    protected virtual void LoadUseBaseInfo()
    {
        if (this._useBaseInfo != null) return;
        this._useBaseInfo = transform.GetComponentInChildren<UseBaseInfo>();
        Debug.LogWarning(transform.name + ": LoadUseInfo", gameObject);
    }
    private void LoadCircleCollider2D()
    {
        if (this._cc != null) return;
        this._cc = transform.GetComponent<CircleCollider2D>();
        this._cc.radius = this._useBaseInfo.useProfile.radiusCollider;
        Debug.LogWarning(transform.name + ": LoadCircleCollider2D", gameObject);
    }

/*    protected virtual void LoadEquipmentInventory()
    {
        foreach (string useType in useTypes)
        {
            string resPath = "ItemProfiles/Use/" + useType + "/" + transform.name;
            UseProfileSO useProfile = Resources.Load<UseProfileSO>(resPath);
            this.useInformation.useProfile = useProfile;
            if (useProfile != null) break;
        }
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }*/
    

    /*    public virtual void SetItemInventory(ItemInventory itemInventory)
        {
            this.itemInventory = itemInventory;
        }*/
}
