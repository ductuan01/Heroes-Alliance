using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerSkillCtrl : SecondMonoBehaviour
{
    private static UIPlayerSkillCtrl _instance;
    public static UIPlayerSkillCtrl Instance => _instance;

    [SerializeField] private SkillInfoCtrl _skillInfoCtrl;
    public SkillInfoCtrl SkillInfoCtrl => _skillInfoCtrl;

    private bool isOpen = true;

    protected override void Awake()
    {
        base.Awake();
        if (UIPlayerSkillCtrl._instance != null) Debug.LogError("Only 1 UIPlayerStatsCtrl allow to exist");
        UIPlayerSkillCtrl._instance = this;

        this.Toggle();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSkillInfoCtrl();
    }

    private void LoadSkillInfoCtrl()
    {
        if (this._skillInfoCtrl != null) return;
        this._skillInfoCtrl = transform.GetComponentInChildren<SkillInfoCtrl>();
        Debug.LogWarning(transform.name + ": LoadSkillInfoCtrl", gameObject);
    }

    private void Update()
    {
        if (InputManager.Instance.GetKeyDown(KeybindingActions.Skills))
        {
            Debug.Log("Open Skill");
            this.Toggle();
        }
    }

    public virtual void Toggle()
    {
        this.isOpen = !this.isOpen;
        if (this.isOpen) this.gameObject.SetActive(true);
        else this.gameObject.SetActive(false);
    }
}
