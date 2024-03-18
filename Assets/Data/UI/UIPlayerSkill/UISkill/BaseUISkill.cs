using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseUISkill : SecondMonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _SkillLevel;

    [SerializeField] private Image _SkillImage;
    protected abstract string _NameSkill { get; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTextMeshPro();
        this.LoadImage();
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
        this._SkillImage.sprite = Resources.Load<SkillProfileSO>("ItemProfiles/Skills/Job1/" + _NameSkill).skillSprite;
        Debug.LogWarning(transform.name + ": LoadTextMeshPro", gameObject);
    }

    public void SetSkillLevel(int skillLevel)
    {
        this._SkillLevel.SetText(skillLevel.ToString());
    }
}
