using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISkillInfo : SecondMonoBehaviour
{
    [SerializeField] private SkillProfileSO _skillProfile = null;
    public SkillProfileSO skillProfile => _skillProfile;

    List<string> skillJobs = new List<string> { "Job1", "Job2" };

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSkillProfile();
    }

    private void LoadSkillProfile()
    {
        if (this._skillProfile != null) return;
        foreach (string skillJob in skillJobs)
        {
            string resPath = "Skills/" + skillJob + "/" + transform.name;
            this._skillProfile = Resources.Load<SkillProfileSO>(resPath);
            if (_skillProfile != null) break;
        }
        Debug.LogWarning(transform.name + ": LoadSkillProfile", gameObject);
    }
}
