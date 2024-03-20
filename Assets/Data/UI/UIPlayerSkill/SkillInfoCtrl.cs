using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfoCtrl : SecondMonoBehaviour
{
    private static SkillInfoCtrl instance;
    public static SkillInfoCtrl Instance => instance;

    [SerializeField] private TextMeshProUGUI _skillName;
    [SerializeField] private Image _skillImage;
    [SerializeField] private TextMeshProUGUI _skillDescription;
    [SerializeField] private TextMeshProUGUI _skillInfo;

    protected override void Awake()
    {
        base.Awake();
        if (SkillInfoCtrl.instance != null) Debug.LogError("Only 1 ShowSkillInfoCtrl allow to exist");
        SkillInfoCtrl.instance = this;

        transform.gameObject.SetActive(false);
/*        uiEquip.gameObject.SetActive(false);
        uiUse.gameObject.SetActive(false);*/
    }
    /*   protected override void LoadComponents()
       {
           base.LoadComponents();
           this.LoadUIEquipInfoCtrl();*/
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSkillNameText();
        this.LoadSkillImage();
        this.LoadSkillDescription();
        this.LoadSkillInfo();
    }

    protected virtual void LoadSkillNameText()
    {
        if (this._skillName != null) return;
        this._skillName = transform.Find("SkillName").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadSkillNameText", gameObject);
    }

    protected virtual void LoadSkillImage()
    {
        if (this._skillImage != null) return;
        this._skillImage = transform.Find("BackGround").Find("SkillImage").GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadSkillImage", gameObject);
    }

    private void LoadSkillDescription()
    {
        if (this._skillDescription != null) return;
        this._skillDescription = transform.Find("SkillDescription").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadSkillDescription", gameObject);
    }
    private void LoadSkillInfo()
    {
        if (this._skillInfo != null) return;
        this._skillInfo = transform.Find("SkillInfo").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadSkillInfo", gameObject);
    }

    public virtual void SetSkillName(UISkillInfo skillInfo)
    {
        if (skillInfo == null) return;
        string nameSkill = skillInfo.skillProfile.skillName;
        this._skillName.SetText(nameSkill);
    }

    public virtual void SetItemImage(UISkillInfo skillInfo)
    {
        if (skillInfo == null) return;
        this._skillImage.sprite = skillInfo.skillProfile.skillSprite;
    }
    public virtual void SetSkillDescription(UISkillInfo skillInfo)
    {
        if (skillInfo == null) return;
        string description = skillInfo.skillProfile.Description;
        this._skillDescription.SetText(description);
    }
    public virtual void SetSkillInfo(UISkillInfo skillInfo)
    {
        if (skillInfo == null) return;
        int currentLevel = 0;

        foreach(Transform skill in PlayerSkills.Instance.Skills)
        {
            if (skill.name == skillInfo.skillProfile.skillCode.ToString())
            {
                currentLevel = skill.GetComponentInChildren<SkillInfo>().CurrentSkillLevel;
                //skillType = skill.GetComponentInChildren<SkillInfo>().SkillProfile.skillType;
            }
        }

        if(skillInfo.skillProfile.skillType == SkillType.ActiveSkill)
        {
            this._skillInfo.SetText("Current Level " + currentLevel + ": " + skillInfo.skillProfile.InfoActiveSkill[currentLevel].infoSkill +
                      "\nNext Level " + (currentLevel + 1) + ": " + skillInfo.skillProfile.InfoActiveSkill[currentLevel + 1].infoSkill);
        }
        if (skillInfo.skillProfile.skillType == SkillType.PassiveBuff)
        {
            this._skillInfo.SetText("Current Level " + currentLevel + ": " + skillInfo.skillProfile.InfoPassiveSkill[currentLevel].infoSkill +
                      "\nNext Level " + (currentLevel + 1) + ": " + skillInfo.skillProfile.InfoPassiveSkill[currentLevel + 1].infoSkill);
        }

        /*        this._skillInfo.SetText("Current Level " + currentLevel + ": " + skillInfo.skillProfile.InfoActiveSkill[currentLevel].infoSkill);
                ("Next Level " + (currentLevel + 1) + ": " + skillInfo.skillProfile.InfoActiveSkill[currentLevel + 1].infoSkill);*/
        /*        if (currentLevel == 0)
                {
                    this._skillInfo.SetText("Next Level " + (currentLevel + 1) + ": " + skillInfo.skillProfile.InfoActiveSkill[currentLevel].infoSkill);
                }
                if (currentLevel != 0)
                {

                }*/
    }
}

/*    protected virtual void LoadUIEquipInfoCtrl()
    {
        if (this.uiEquip != null) return;
        this.uiEquip = transform.GetComponentInChildren<UIEquipInfoCtrl>();
        Debug.LogWarning(transform.name + ": LoadUIEquipInfoCtrl", gameObject);
    }*/

    



