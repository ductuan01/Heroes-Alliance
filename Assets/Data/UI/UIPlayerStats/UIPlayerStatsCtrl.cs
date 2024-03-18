using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerStatsCtrl : SecondMonoBehaviour
{
    private static UIPlayerStatsCtrl _instance;
    public static UIPlayerStatsCtrl Instance => _instance;

    [SerializeField] private PlayerStatsCtrl _playerStatsCtrl;

    [SerializeField] protected AbilityManager abilityManager;

    private bool isOpen = true;

    protected override void Awake()
    {
        base.Awake();
        if (UIPlayerStatsCtrl._instance != null) Debug.LogError("Only 1 UIPlayerStatsCtrl allow to exist");
        UIPlayerStatsCtrl._instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerStatsCtrl();
        this.LoadAbilityManager();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.abilityManager.OnAbilityChange += SetUIPlayerStats;
    }

    protected override void OnDisable()
    {
        base.OnEnable();
        this.abilityManager.OnAbilityChange -= SetUIPlayerStats;
    }

    protected virtual void LoadAbilityManager()
    {
        if (this.abilityManager != null) return;
        this.abilityManager = transform.GetComponent<AbilityManager>();
        Debug.Log(transform.name + ": LoadAbilityManager", gameObject);
    }

    protected virtual void LoadPlayerStatsCtrl()
    {
        if (this._playerStatsCtrl != null) return;
        this._playerStatsCtrl = transform.GetComponentInChildren<PlayerStatsCtrl>();
        Debug.LogWarning(transform.name + "LoadPlayerStatsCtrl" + gameObject);
    }

    protected override void Start()
    {
        base.Start();
        this.SetUIPlayerStats();
        this.Toggle();

    }
    private void SetUIPlayerStats()
    {
        PlayerStats playerStats = PlayerStats.Instance;
        this._playerStatsCtrl.transform.Find("NAME").GetComponent<PlayerStat>().SetText("Tuan");
        this._playerStatsCtrl.transform.Find("JOB").GetComponent<PlayerStat>().SetText("Knight");
        this._playerStatsCtrl.transform.Find("GUILD").GetComponent<PlayerStat>().SetText("????");
        this._playerStatsCtrl.transform.Find("FAME").GetComponent<PlayerStat>().SetText("????");
        this._playerStatsCtrl.transform.Find("MinDamage").GetComponent<PlayerStat>().SetText(playerStats.MinDamage.ToString());
        this._playerStatsCtrl.transform.Find("MaxDamage").GetComponent<PlayerStat>().SetText(playerStats.MaxDamage.ToString());
        this._playerStatsCtrl.transform.Find("HP").GetComponent<PlayerStat>().SetText(playerStats.currentHP + "/" + playerStats.TotalHP);
        this._playerStatsCtrl.transform.Find("MP").GetComponent<PlayerStat>().SetText(playerStats.currentMP + "/" + playerStats.TotalMP);
        this._playerStatsCtrl.transform.Find("AbilityPoint").GetComponent<PlayerStat>().SetText(playerStats.AbilityPoint.ToString());
        this._playerStatsCtrl.transform.Find("STR").GetComponent<PlayerStat>().SetPlayerStats(playerStats.BaseSTR, playerStats.EtcSTR);
        this._playerStatsCtrl.transform.Find("DEX").GetComponent<PlayerStat>().SetPlayerStats(playerStats.BaseDEX, playerStats.EtcDEX);
        this._playerStatsCtrl.transform.Find("INT").GetComponent<PlayerStat>().SetPlayerStats(playerStats.BaseINT, playerStats.EtcINT);
        this._playerStatsCtrl.transform.Find("LUK").GetComponent<PlayerStat>().SetPlayerStats(playerStats.BaseLUK, playerStats.EtcLUK);
    }

    public void Toggle()
    {
        PlayerStats.Instance.SetDamage();
        this.isOpen = !this.isOpen;
        if (this.isOpen) this.gameObject.SetActive(true);
        else this.gameObject.SetActive(false);
    }
}