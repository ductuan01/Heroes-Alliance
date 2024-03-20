using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovingMaxHPIncrease : PlayerSkillsAbstract
{
    private static ImprovingMaxHPIncrease _instance;
    public static ImprovingMaxHPIncrease Instance => _instance;

    [SerializeField] private SkillInfo _skillInfo;
    public SkillInfo SkillInfo => _skillInfo;

    protected override void Awake()
    {
        base.Awake();
        if (ImprovingMaxHPIncrease._instance != null) Debug.LogError("Only 1 ImprovingMaxHPIncrease allow to exist");
        ImprovingMaxHPIncrease._instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSkillInfo();
    }

    private void LoadSkillInfo()
    {
        if (this._skillInfo != null) return;
        this._skillInfo = transform.GetComponentInChildren<SkillInfo>();
        Debug.LogWarning(transform.name + ": LoadSkillInfo", gameObject);
    }

    protected override void Start()
    {
        this.ImprovingMaxHPSkill();
    }

    public void ImprovingMaxHPSkill()
    {
        int SkillMaxHP = _skillInfo.SkillProfile.InfoPassiveSkill[this._skillInfo.CurrentSkillLevel].increasesMaxHP * PlayerLevel.Instance.CurrentLevel;
        PlayerSkills.PlayerCtrl.PlayerStats.RiseSkillMaxHP(SkillMaxHP);
    }

    public void RaiseSkillLevel()
    {
        if (!PlayerSkills.ReduceSkillPoint()) return;
        this._skillInfo.RaiseSkillLevel();
        UIImprovingMaxHPIncrease.Instance.SetSkillLevel(this._skillInfo.CurrentSkillLevel);
    }
}
