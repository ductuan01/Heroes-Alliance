using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : PlayerAbstract
{
    private static PlayerStats _instance;
    public static PlayerStats Instance => _instance;

    [SerializeField] private string _playerName;
    public string playerName => _playerName;

    //[SerializeField] private string Job = "Warrior";

    [SerializeField] private int _MaxDamage;
    public int MaxDamage => _MaxDamage;

    [SerializeField] private int _MinDamage;
    public int MinDamage => _MinDamage;

    [SerializeField] private int _TotalHP;
    public int TotalHP => _TotalHP;

    [SerializeField] private int _TotalMP;
    public int TotalMP => _TotalMP;

    [SerializeField] private int _currentHP = 50;
    public int currentHP => _currentHP;

    [SerializeField] private int _currentMP = 50;
    public int currentMP => _currentMP;

    [SerializeField] private int _MaxHP = 500;
    public int MaxHp => _MaxHP;

    [SerializeField] private int _MaxMP = 500;
    public int MaxMp => _MaxMP;

    [SerializeField] private int Def = 50;

    [SerializeField] private int _CriticalRate = 25;
    public int CriticalRate => _CriticalRate;

    [SerializeField] private float _attackSpeed = 4f;
    public float AtttackSpeed => _attackSpeed;

    [SerializeField] private int _AbilityPoint = 50;
    public int AbilityPoint => _AbilityPoint;

    // just ability
    [SerializeField] private int _BaseSTR = 5;
    public int BaseSTR => _BaseSTR;
    [SerializeField] private int _BaseDEX = 5;
    public int BaseDEX => _BaseDEX;
    [SerializeField] private int _BaseINT = 5;
    public int BaseINT => _BaseINT;
    [SerializeField] private int _BaseLUK = 5;
    public int BaseLUK => _BaseLUK;
    // just ability

    // for equip or something like that;
    [SerializeField] private int _EtcSTR = 0;
    public int EtcSTR => _EtcSTR;
    [SerializeField] private int _EtcDEX = 0;
    public int EtcDEX => _EtcDEX;
    [SerializeField] private int _EtcINT = 0;
    public int EtcINT => _EtcINT;
    [SerializeField] private int _EtcLUK = 0;
    public int EtcLUK => _EtcLUK;

    [SerializeField] private int _EtcMaxHP = 0;
    public int EtcMaxHP => _EtcMaxHP;

    [SerializeField] private int _EtcMaxMP = 0;
    public int EtcMaxMP => _EtcMaxMP;
    // for equip or something like that;

    // for skill
    [SerializeField] private int _SkillMaxHP = 0;
    public int SkillMaxHP => _SkillMaxHP;

    [SerializeField] private int _EtcAttPower = 0;
    [SerializeField] private int _EtcMattPower = 0;

    [SerializeField] private int AttPower = 50;
    [SerializeField] private int MattPower = 5;

    private bool _isDead = false;
    public bool IsDead => _isDead;

    protected override void OnEnable()
    {
        base.OnEnable();
        //abilityManager.OnAbilityChange += HandleAbilityChange;
    }

    protected override void Awake()
    {
        base.Awake();
        if (PlayerStats._instance != null) Debug.LogError("Only 1 PlayerStats allow to exist");
        PlayerStats._instance = this;
        _playerName = "Tuan";
    }

    protected override void Start()
    {
        base.Start();
        this.SetDamage();
        this.SetTotalHPMP();
        HPMPBarManager.instance.HPMPBarChange(this._currentHP, this._TotalHP, this._currentMP, this._TotalMP);
        AbilityManager.Instance.AbilityChange();
    }
    public virtual void RaiseBaseAfterLevelUp()
    {
        this._AbilityPoint += 5;
        this._MaxHP += 25;
        this._MaxMP += 5;
        this._currentHP = this._MaxHP;
        this._currentMP = this._MaxMP;
        this.Def += 10;
    }

    public void RiseHP(int amount)
    {
        this._currentHP += amount;
        if (this._currentHP > this._TotalHP) this._currentHP = this._TotalHP;
        HPMPBarManager.instance.HPMPBarChange(this._currentHP, this._TotalHP, this._currentMP, this._TotalMP);
        AbilityManager.Instance.AbilityChange();
    }

    public void RiseMP(int amount)
    {
        this._currentMP += amount;
        if (this._currentMP > this._TotalMP) this._currentMP = this._TotalMP;
        HPMPBarManager.instance.HPMPBarChange(this._currentHP, this._TotalHP, this._currentMP, this._TotalMP);
        AbilityManager.Instance.AbilityChange();
    }

    public void RiseSkillMaxHP(int SkillMaxHP)
    {
        this._SkillMaxHP = SkillMaxHP;
        Debug.Log(this._SkillMaxHP);
        this.SetTotalHPMP();
        HPMPBarManager.instance.HPMPBarChange(this._currentHP, this._TotalHP, this._currentMP, this._TotalMP);
        AbilityManager.Instance.AbilityChange();
    }

    public void ReduceHP(int damage)
    {
        if (this._isDead) return;
        this._currentHP -= damage;// - damage * this.Def/100;
        if (this._currentHP < 0)
        {
            this._currentHP = 0;
            this._isDead = true;

            PlayerCtrl.PlayerLevel.ReduceExpAfterDead();
            PlayerCtrl.PlayerDead.Active();
            UIPlayerDeadCtrl.Instance.Toggle();
        }
        HPMPBarManager.instance.HPMPBarChange(this._currentHP, this._TotalHP, this._currentMP, this._TotalMP);
        AbilityManager.Instance.AbilityChange();
    }

    public bool ReduceMP(int mpCost)
    {
        if (mpCost > this.currentMP) return false;
        this._currentMP -= mpCost;
        if (this._currentMP < 0) this._currentMP = 0;
        HPMPBarManager.instance.HPMPBarChange(this._currentHP, this._TotalHP, this._currentMP, this._TotalMP);
        AbilityManager.Instance.AbilityChange();
        return true;
    }

    public void OnDead()
    {
        if (!this._isDead) return;
        PlayerCtrl.transform.position = new Vector3(0f, 0f, 0f);
        this._isDead = false;

        PlayerCtrl.PlayerModel.gameObject.SetActive(true);
        PlayerCtrl.PlayerDead.gameObject.SetActive(false);

        UIPlayerDeadCtrl.Instance.Toggle();
    }

    public void RaiseSTRAbility()
    {
        if (this._AbilityPoint < 1) return;
        this._BaseSTR += 1;
        this._AbilityPoint -= 1;
        this.SetDamage();
        AbilityManager.Instance.AbilityChange();
    }
    public void RaiseDEXAbility()
    {
        if (this._AbilityPoint < 1) return;
        this._BaseDEX += 1;
        this._AbilityPoint -= 1;
        this.SetDamage();
        AbilityManager.Instance.AbilityChange();
    }
    public void RaiseINTAbility()
    {
        if (this._AbilityPoint < 1) return;
        this._BaseINT += 1;
        this._AbilityPoint -= 1;
        this.SetDamage();
        AbilityManager.Instance.AbilityChange();
    }
    public void RaiseLUKAbility()
    {
        if (this._AbilityPoint < 1) return;
        this._BaseLUK += 1;
        this._AbilityPoint -= 1;
        this.SetDamage();
        AbilityManager.Instance.AbilityChange();
    }

    public void SetDamage()
    {
        int totalSTR = this._BaseSTR + this._EtcSTR;
        int totalDEX = this._BaseDEX + this._EtcDEX;
        int totalAttPower = this.AttPower + this._EtcAttPower;

        this._MinDamage = (totalSTR * 4 + totalDEX * 2) * (int)(totalAttPower * 0.9f) / 100;
        this._MaxDamage = (totalSTR * 5 + totalDEX * 3) * totalAttPower / 100;
        Debug.Log(this._MinDamage);
        Debug.Log(this._MaxDamage);
        AbilityManager.Instance.AbilityChange();
    }
    
    public void SetTotalHPMP()
    {
        this._TotalHP = this._MaxHP + this._EtcMaxHP + this._SkillMaxHP;
        Debug.Log(_TotalHP);
        this._TotalMP = this._MaxMP + this._EtcMaxMP;
        HPMPBarManager.instance.HPMPBarChange(this._currentHP, this._TotalHP, this._currentMP, this._TotalMP);
    }

    public void SetEtcStats(int STR, int DEX, int INT, int LUK, int MaxHP, int MaxMP, int AttPower, int MattPower)
    {
        this.SetEtcSTR(STR);
        this.SetEtcDEX(DEX);
        this.SetEtcINT(INT);
        this.SetEtcLUK(LUK);
        this.SetEtcMaxHP(MaxHP);
        this.SetEtcMaxMP(MaxMP);
        this.SetEtcAttPower(AttPower);
        this.SetEtcMattPower(MattPower);
        this.SetDamage();
        this.SetTotalHPMP();
    }

    public void SetEtcSTR(int STR)
    {
        this._EtcSTR = STR;
    }
    public void SetEtcDEX(int DEX)
    {
        this._EtcDEX = DEX;
    }
    public void SetEtcINT(int INT)
    {
        this._EtcINT = INT;
    }
    public void SetEtcLUK(int LUK)
    {
        this._EtcLUK = LUK;
    }
    public void SetEtcMaxHP(int MaxHP)
    {
        this._EtcMaxHP = MaxHP;
    }
    public void SetEtcMaxMP(int MaxMP)
    {
        this._EtcMaxMP = MaxMP;
    }
    public void SetEtcAttPower(int AttPower)
    {
        this._EtcAttPower = AttPower;
    }
    public void SetEtcMattPower(int MattPower)
    {
        this._EtcMattPower = MattPower;
    }
}
