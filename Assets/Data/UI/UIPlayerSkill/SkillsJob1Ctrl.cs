using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsJob1Ctrl : SecondMonoBehaviour
{
    private static SkillsJob1Ctrl _instance;
    public static SkillsJob1Ctrl Instance => _instance;

    [SerializeField] private UISlashBlast _uiSlashBlast;
    [SerializeField] private UIWarLeap _uiWarLeap;
    [SerializeField] private UIEndure _uiEndure;
    [SerializeField] private UIImprovingMaxHPIncrease _uiImprovingMaxHPIncrease;

    protected override void Awake()
    {
        base.Awake();
        if (SkillsJob1Ctrl._instance != null) Debug.LogError("Only 1 SkillsJob1Ctrl allow to exist");
        SkillsJob1Ctrl._instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUISlashBlast();
        this.LoadUIWarLeap();
        this.LoadUIEndure();
        this.LoadUIImprovingMaxHPIncrease();
    }

    private void LoadUISlashBlast()
    {
        if (this._uiSlashBlast != null) return;
        this._uiSlashBlast = transform.GetComponentInChildren<UISlashBlast>();
        Debug.LogWarning(transform.name + ": LoadUISlashBlast", gameObject);
    }
    private void LoadUIWarLeap()
    {
        if (this._uiWarLeap != null) return;
        this._uiWarLeap = transform.GetComponentInChildren<UIWarLeap>();
        Debug.LogWarning(transform.name + ": LoadUIWarLeap", gameObject);
    }
    private void LoadUIEndure()
    {
        if (this._uiEndure != null) return;
        this._uiEndure = transform.GetComponentInChildren<UIEndure>();
        Debug.LogWarning(transform.name + ": LoadUIEndure", gameObject);
    }
    private void LoadUIImprovingMaxHPIncrease()
    {
        if (this._uiImprovingMaxHPIncrease != null) return;
        this._uiImprovingMaxHPIncrease = transform.GetComponentInChildren<UIImprovingMaxHPIncrease>();
        Debug.LogWarning(transform.name + ": LoadUIImprovingMaxHPIncrease", gameObject);
    }

    public void LoadUISkillInfo()
    {
        this._uiSlashBlast.SetSkillLevel(SlashBlast.Instance.SkillInfo.CurrentSkillLevel);
        this._uiWarLeap.SetSkillLevel(WarLeap.Instance.SkillInfo.CurrentSkillLevel);
        this._uiEndure.SetSkillLevel(Endure.Instance.SkillInfo.CurrentSkillLevel);
        this._uiImprovingMaxHPIncrease.SetSkillLevel(ImprovingMaxHPIncrease.Instance.SkillInfo.CurrentSkillLevel);
    }
}
