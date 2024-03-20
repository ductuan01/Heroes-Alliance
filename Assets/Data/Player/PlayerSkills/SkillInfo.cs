using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo : SecondMonoBehaviour
{
    [SerializeField] private SkillProfileSO _skillProfile;
    public SkillProfileSO SkillProfile => _skillProfile;

    [SerializeField] private int _currentSkillLevel = 0;
    public int CurrentSkillLevel => _currentSkillLevel;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSkillProfile();
    }

    private void LoadSkillProfile()
    {
        if (this._skillProfile != null) return;
        string resPath = "Skills/Job1/" + transform.parent.name;
        this._skillProfile = Resources.Load<SkillProfileSO>(resPath);
        Debug.LogWarning(transform.name + ": LoadSkillProfile", gameObject);
    }

    public void RaiseSkillLevel()
    {
        this._currentSkillLevel += 1;
    }

    public void SetCurrentLevel(int level)
    {
        this._currentSkillLevel = level;
    }
}
