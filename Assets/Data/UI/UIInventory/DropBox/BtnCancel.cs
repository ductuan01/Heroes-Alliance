using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCancel : BaseButton
{
    protected override void OnClick()
    {
        Debug.Log("Cancel");
        UIInventoryCtrl.Instance.amountDropBox.HideBox();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            button.onClick.Invoke();
        }
    }
}
