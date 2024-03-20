using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseUISkill : SecondMonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _SkillLevel;

    [SerializeField] private Image _SkillImage;
    protected abstract string NameSkill { get; }

    [SerializeField] public SkillPointManager _skillPointManager;
    //public SkillPointManager SkillPointManager => _skillPointManager;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTextMeshPro();
        this.LoadImage();
        this.LoadSkillPointManager();
    }

    private void LoadTextMeshPro()
    {
        if (this._SkillLevel != null) return;
        this._SkillLevel = transform.Find("SkillLevel").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadTextMeshPro", gameObject);
    }
    private void LoadImage()
    {
        if (this._SkillImage != null) return;
        this._SkillImage = transform.Find("SkillSlot").Find("SkillImage").GetComponent<Image>();
        this._SkillImage.sprite = Resources.Load<SkillProfileSO>("Skills/Job1/" + NameSkill).skillSprite;
        Debug.LogWarning(transform.name + ": LoadTextMeshPro", gameObject);
    }

    private void LoadSkillPointManager()
    {
        if (this._skillPointManager != null) return;
        this._skillPointManager = GameObject.Find("PlayerSkills").GetComponentInChildren<SkillPointManager>();
    }

    public void SetSkillLevel(int skillLevel)
    {
        this._SkillLevel.SetText(skillLevel.ToString());
    }
}
