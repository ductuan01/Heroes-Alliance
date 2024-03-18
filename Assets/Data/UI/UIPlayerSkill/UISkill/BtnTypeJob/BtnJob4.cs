using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnJob4 : BaseButton
{
    protected override void OnClick()
    {
        Transform transform = DamagePopupSpawner.Instance.Spawn(DamagePopupSpawner.damagePopup, this.transform.position, Quaternion.identity);
        DamagePopupCtrl Text = transform.GetComponent<DamagePopupCtrl>();
        Text.Text("Not available");
        transform.gameObject.SetActive(true);
    }
}
