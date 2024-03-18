using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerDeadCtrl : SecondMonoBehaviour
{
    private static UIPlayerDeadCtrl _instance;
    public static UIPlayerDeadCtrl Instance => _instance;

    private bool IsOpen = true;
    protected override void Awake()
    {
        base.Awake();
        if (UIPlayerDeadCtrl._instance != null) Debug.LogError("Only 1 UIPlayerDeadCtrl allow to exist");
        UIPlayerDeadCtrl._instance = this;
        this.Toggle();
    }
    public void Toggle()
    {
        this.IsOpen = !this.IsOpen;
        if (IsOpen) OpenUI();
        else CloseUI();
    }
    
    public void OpenUI()
    {
        transform.gameObject.SetActive(true);
    }
    public void CloseUI()
    {
        transform.gameObject.SetActive(false);
    }
}
