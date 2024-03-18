using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCtrl : SecondMonoBehaviour
{
    [SerializeField] private MonsterMovement _monsterMovement;
    public MonsterMovement MonsterMovement => _monsterMovement;

    [SerializeField] private MonsterStats _monsterStats;
    public MonsterStats MonsterStats => _monsterStats;

    [SerializeField] private MonsterSO _monsterSO;
    public MonsterSO MonsterSO => _monsterSO;

    [SerializeField] private MonsterAttack _monsterAttack;
    public MonsterAttack MonsterAttack => _monsterAttack;

    [SerializeField] private MonsterDrop _monsterDrop;
    public MonsterDrop MonsterDrop => _monsterDrop;

    [SerializeField] private MonsterAnimation _monsterAnim;
    public MonsterAnimation MonsterAnim => _monsterAnim;

    [SerializeField] public MonsterDamageReceiver _monsterDamageReceiver;

    [SerializeField] public Transform _HPBar;

    [SerializeField] public Slider _slider;

    [SerializeField] public TextMeshProUGUI _monsterName;

    [SerializeField] public TextMeshProUGUI _monsterLevel;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMonsterMovement();
        this.LoadMonsterStats();
        this.LoadMonsterSO();
        this.LoadMonsterAttack();
        this.LoadMonsterDamageReceiver();
        this.LoadHPBar();
        this.LoadSlider();
        this.LoadNameAndLevel();
        //this.LoadFollowTarget();
    }
    private void LoadMonsterMovement()
    {
        if (this._monsterMovement != null) return;
        this._monsterMovement = transform.GetComponentInChildren<MonsterMovement>();
        Debug.LogWarning(transform.name + ": LoadMonsterMovement", gameObject);
    }

    private void LoadMonsterStats()
    {
        if (this._monsterStats != null) return;
        this._monsterStats = transform.GetComponentInChildren<MonsterStats>();
        Debug.LogWarning(transform.name + ": LoadMonsterStats", gameObject);
    }
    private void LoadMonsterSO()
    {
        if (this._monsterSO != null) return;
        string resPath = "Monster/" + transform.name;
        this._monsterSO = Resources.Load<MonsterSO>(resPath);
        Debug.LogWarning(transform.name + ": LoadMonsterSO", gameObject);
    }
    
    private void LoadMonsterAttack()
    {
        if (this._monsterAttack != null) return;
        this._monsterAttack = transform.GetComponentInChildren<MonsterAttack>();
        Debug.LogWarning(transform.name + ": LoadMonsterAttack", gameObject);
    }
    private void LoadMonsterDamageReceiver()
    {
        if (this._monsterDamageReceiver != null) return;
        this._monsterDamageReceiver = transform.GetComponentInChildren<MonsterDamageReceiver>();
        Debug.LogWarning(transform.name + ": LoadDamageAble", gameObject);
    }

    private void LoadHPBar()
    {
        if (this._HPBar != null) return;
        this._HPBar = transform.Find("HPBar");
        Debug.LogWarning(transform.name + ": LoadFollowTarget", gameObject);
    }

    private void LoadSlider()
    {
        if (this._slider != null) return;
        this._slider = transform.Find("HPBar").GetComponentInChildren<Slider>();
        Debug.LogWarning(transform.name + ": LoadSlider", gameObject);
    }

    private void LoadNameAndLevel()
    {
        if (this._monsterName != null || this._monsterLevel) return;
        this._monsterName = transform.Find("NameAndLevel").Find("Name").GetComponentInChildren<TextMeshProUGUI>();
        this._monsterLevel = transform.Find("NameAndLevel").Find("Level").GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadNameAndLevel", gameObject);
    }


    /// <summary>
    /// //////////////////////////////////
    /// </summary>


    /*    private void LoadFollowTarget()
        {
            if (this.followTarget != null) return;
            this.followTarget = transform.GetComponentInChildren<FollowTarget>();
            Debug.LogWarning(transform.name + ": LoadFollowTarget", gameObject);
        }*/


}
