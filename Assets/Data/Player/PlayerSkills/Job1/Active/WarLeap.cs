using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarLeap : PlayerSkillsAbstract
{
    private static WarLeap _instance;
    public static WarLeap Instance => _instance;

    [SerializeField] private SkillInfo _skillInfo;

    private bool _canUse = true;

    protected override void Awake()
    {
        if (WarLeap._instance != null) Debug.LogError("Only 1 WarLeap allow to exist");
        WarLeap._instance = this;
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

    void FixedUpdate()
    {
        if (PlayerSkills.PlayerCtrl.PlayerMovement.IsGrounded)
        {
            _canUse = true;
        }
    }

    private void Update()
    {
        if (InputManager.Instance.GetKeyDown(KeybindingActions.WarLeap) && this._skillInfo.CurrentSkillLevel > 0 && _canUse)
        {
            if (!PlayerSkills.PlayerCtrl.PlayerMovement.IsJumping) return;
            int MPCost = _skillInfo.SkillProfile.InfoActiveSkill[this._skillInfo.CurrentSkillLevel].MPCost;
            if (!PlayerSkills.PlayerCtrl.PlayerStats.ReduceMP(MPCost)) return;
            this.WarLeapSkill(_skillInfo.SkillProfile.InfoActiveSkill[this._skillInfo.CurrentSkillLevel].Force);
            Transform transform = ImpactSpawner.Instance.Spawn(ImpactSpawner.impact_2, this.transform.position, Quaternion.Euler(0f, 0f, -90f));
            transform.gameObject.SetActive(true);
            this._canUse = false;
        }
    }

    private void WarLeapSkill(float Force)
    {
        Rigidbody2D Rb = PlayerSkills.PlayerCtrl.Rb;
        PlayerSkills.PlayerCtrl.Rb.AddForce(Rb.transform.up * Force, ForceMode2D.Impulse);
    }

    public void RaiseSkillLevel()
    {
        if (!PlayerSkills.ReduceSkillPoint()) return;
        this._skillInfo.RaiseSkillLevel();
        UIWarLeap.Instance.SetSkillLevel(this._skillInfo.CurrentSkillLevel);
    }
}
