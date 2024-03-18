using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopupSpawner : Spawner
{
    private static DamagePopupSpawner instance;
    public static DamagePopupSpawner Instance => instance;

    public static string damagePopup = "pfDamagePopup";

/*    [SerializeField] private TextMeshPro OutLine;
    [SerializeField] private TextMeshPro Text;*/

    protected override void Awake()
    {
        base.Awake();
        if (DamagePopupSpawner.instance != null) Debug.LogError("Only 1 DamagePopupSpawner allow to exist");
        DamagePopupSpawner.instance = this;
    }

/*    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }

    private void LoadText()
    {
        if (this.OutLine != null || this.Text != null) return;
        this.OutLine = transform.GetComponent<TextMeshPro>();
        this.Text = transform.Find("pfDamagePopup_1").GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount)
    {
        this.OutLine.SetText(damageAmount.ToString());
        this.Text.SetText(damageAmount.ToString());
    }*/

/*    public static DamagePopup Create(Vector3 position, int damageAmount)
    {
        Transform damagePopupTransform = Instantiate(DamagePopup da, position, Quaternion.identity);
    }*/
}
