using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISkillPoint : SecondMonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _skillPoint;

    [SerializeField] private SkillPointManager _skillPointManager;
    public SkillPointManager skillPointManager => _skillPointManager;

    protected override void Start()
    {
        base.Start();
        this.ResetSkillPoint(PlayerSkills.Instance.SkillPoint);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTextMeshPro();
        this.LoadSkillPointManager();
    }

    private void LoadTextMeshPro()
    {
        if (this._skillPoint != null) return;
        this._skillPoint = transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadTextMeshPro", gameObject);
    }
    protected virtual void LoadSkillPointManager()
    {
        if (_skillPointManager != null) return;
        this._skillPointManager = GameObject.Find("PlayerSkills").GetComponentInChildren<SkillPointManager>();
        Debug.LogWarning(transform.name + ": LoadExperienceManager", gameObject);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _skillPointManager.OnSkillPointChange += ResetSkillPoint;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _skillPointManager.OnSkillPointChange += ResetSkillPoint;
    }

    private void ResetSkillPoint(int skillPoint)
    {
        this._skillPoint.SetText(skillPoint.ToString());
    }
}
