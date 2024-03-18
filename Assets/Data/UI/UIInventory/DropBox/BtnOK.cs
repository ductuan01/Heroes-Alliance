using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOK : BaseButton
{
    protected override void OnClick()
    {
        string Limit = UIInventoryCtrl.Instance.amountDropBox.inputField.text;
        DropItemFromInv.Instance.UseAndEtcDrop(int.Parse(Limit));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            button.onClick.Invoke();
        }
    }
}
