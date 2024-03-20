using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : PlayerAbstract
{
    private static PlayerSkills _instance;
    public static PlayerSkills Instance => _instance;

    [SerializeField] private List<Transform> _skills;
    public List<Transform> Skills => _skills;

    private bool _isAttacking = false;
    public bool IsAttacking => _isAttacking;

    [SerializeField] private int _skillPoint = 0;
    public int SkillPoint => _skillPoint;

    [SerializeField] private SkillPointManager _skillPointManager;
    public SkillPointManager SkillPointManager => _skillPointManager;

    protected override void Awake()
    {
        if (PlayerSkills._instance != null) Debug.LogError("Only 1 PlayerSkills allow to exist");
        PlayerSkills._instance = this;

        this.LoadSkillsData();
    }

    private void LoadSkillsData()
    {
        string resPath = "GameData/PlayerSkills/SkillsData";
        SkillsDataSO skillsDataSO = Resources.Load<SkillsDataSO>(resPath);
        if (skillsDataSO == null) return;
        this._skillPoint = skillsDataSO.skillPoint;

        foreach(Transform skill in this._skills)
        {
            SkillInfo skillInfo = skill.transform.GetComponentInChildren<SkillInfo>();
            if (skillInfo == null) continue;
            foreach (SkillInfoOfData skillInfoOfData in skillsDataSO.job1)
            {
                if (skillInfo.SkillProfile == skillInfoOfData.equipProfile)
                {
                    skillInfo.SetCurrentLevel(skillInfoOfData.currentSkillLevel);
                }
            }
        }
        PlayerCtrl.PlayerStats.LoadStatsData();
    }
    public void SaveSkillsData()
    {
        string resPath = "GameData/PlayerSkills/SkillsData";
        SkillsDataSO skillsDataSO = Resources.Load<SkillsDataSO>(resPath);
        if (skillsDataSO == null) return;
        skillsDataSO.skillPoint = this._skillPoint;
        foreach (Transform skill in this._skills)
        {
            SkillInfo skillInfo = skill.transform.GetComponentInChildren<SkillInfo>();
            if (skillInfo == null) continue;
            foreach (SkillInfoOfData skillInfoOfData in skillsDataSO.job1)
            {
                if (skillInfoOfData.equipProfile == skillInfo.SkillProfile)
                {
                    skillInfoOfData.currentSkillLevel = skillInfo.CurrentSkillLevel;
                }
            }
        }
    }
    public void SkillsDataNewGame()
    {
        string resPath = "GameData/PlayerSkills/SkillsData";
        SkillsDataSO skillsDataSO = Resources.Load<SkillsDataSO>(resPath);
        if (skillsDataSO == null) return;
        skillsDataSO.skillPoint = 0;
        foreach (Transform skill in this._skills)
        {
            SkillInfo skillInfo = skill.transform.GetComponentInChildren<SkillInfo>();
            if (skillInfo == null) continue;
            foreach (SkillInfoOfData skillInfoOfData in skillsDataSO.job1)
            {
                if (skillInfoOfData.equipProfile == skillInfo.SkillProfile)
                {
                    skillInfoOfData.currentSkillLevel = 0;
                }
            }
        }
        this.LoadSkillsData();
        this._skillPointManager.SkillPointChange(SkillPoint);
        SkillsJob1Ctrl.Instance.LoadUISkillInfo();
    }    

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSkillPointManager();
        this.LoadSkills();
    }
    protected virtual void LoadSkillPointManager()
    {
        if (this._skillPointManager != null) return;
        this._skillPointManager = transform.GetComponentInChildren<SkillPointManager>();
        Debug.LogWarning(transform.name + ": LoadSkillPointManager", gameObject);
    }
    private void LoadSkills()
    {   
        if (this._skills.Count > 0) return;
        foreach (Transform Jobs in this.transform)
        {
            foreach(Transform Skill in Jobs)
            {
                this._skills.Add(Skill);
            }
        }
        Debug.LogWarning(transform.name + ": LoadSkills", gameObject);
    }
    public void SetIsAttacking(bool isAttacking)
    {
        this._isAttacking = isAttacking;
    }

    public void AddSkillPointAfterLevelUp()
    {
        this._skillPoint += 3;
        this._skillPointManager.SkillPointChange(SkillPoint);
    }

    public bool ReduceSkillPoint() // for raise up level skill
    {
        if (this._skillPoint <= 0) return false;
        this._skillPoint -= 1;
        this._skillPointManager.SkillPointChange(SkillPoint);
        return true;
    }
}
