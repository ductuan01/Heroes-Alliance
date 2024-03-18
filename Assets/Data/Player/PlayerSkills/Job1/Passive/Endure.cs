using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endure : PlayerSkillsAbstract
{
    private static Endure _instance;
    public static Endure Instance => _instance;

    [SerializeField] private SkillInfo _skillInfo;

    protected override void Awake()
    {
        base.Awake();
        if (Endure._instance != null) Debug.LogError("Only 1 Endure allow to exist");
        Endure._instance = this;
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
        StartCoroutine(EndureSkill());
    }

    private IEnumerator EndureSkill()
    {
        while (true)
        {
            if (this._skillInfo.CurrentSkillLevel >= 1)
            {
                PlayerSkills.PlayerCtrl.PlayerStats.RiseHP(_skillInfo.SkillProfile.InfoPassiveSkill[this._skillInfo.CurrentSkillLevel].recoverHP);
            }
            yield return new WaitForSeconds(_skillInfo.SkillProfile.InfoPassiveSkill[this._skillInfo.CurrentSkillLevel].timeLoop);
        }
    }

    public void RaiseLevelSkill()
    {
        if (!PlayerSkills.ReduceSkillPoint()) return;
        this._skillInfo.RaiseSkillLevel();
        UIEndure.Instance.SetSkillLevel(this._skillInfo.CurrentSkillLevel);
    }
}
