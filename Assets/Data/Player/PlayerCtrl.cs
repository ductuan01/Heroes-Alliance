using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : SecondMonoBehaviour
{
    private static PlayerCtrl _instance;
    public static PlayerCtrl Instance => _instance;

    [SerializeField] private Transform _playerModel;
    public Transform PlayerModel => _playerModel;

    [SerializeField] private PlayerMovement _playerMovement;
    public PlayerMovement PlayerMovement => _playerMovement;

    [SerializeField] private PlayerStats _playerStats;
    public PlayerStats PlayerStats => _playerStats;

    [SerializeField] private PlayerSkills _playerSkills;
    public PlayerSkills PlayerSkills => _playerSkills;

    [SerializeField] private PlayerLevel _playerLevel;
    public PlayerLevel PlayerLevel => _playerLevel;

    [SerializeField] private PlayerInventory _playerInventory;
    public PlayerInventory PlayerInventory => _playerInventory;

    [SerializeField] private PlayerEquipInv _playerEquipInv;
    public PlayerEquipInv PlayerEquipInv => _playerEquipInv;

    [SerializeField] private PlayerAnimation _playerAnimation;
    public PlayerAnimation PlayerAnimation => _playerAnimation;

    [SerializeField] public PlayerDead _playerDead;
    public PlayerDead PlayerDead => _playerDead;

    [SerializeField] private Rigidbody2D _rb;
    public Rigidbody2D Rb => _rb;

    [SerializeField] private CapsuleCollider2D _cc;
    public CapsuleCollider2D Cc => _cc;

    [SerializeField] private bool isFacingRight;
    public bool IsFacingRight => isFacingRight;

/*    [SerializeField] protected DamageSender damageSender;
    public DamageSender DamageSender => damageSender;*/

    protected override void Awake()
    {
        base.Awake();
        if (PlayerCtrl._instance != null) Debug.LogError("Only 1 PlayerCtrl allow to exist");
        PlayerCtrl._instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayeModel();
        this.LoadPlayerMovement();
        this.LoadPlayerStats();
        this.LoadPlayerSkills();
        this.LoadPlayerLevel();
        this.LoadPlayerInventory();
        this.LoadPlayerEquipInv();
        this.LoadPlayerAnimation();
        this.LoadPlayerDead();
        this.LoadRigidbody2D();
        this.LoadCollider2D();
    }
    private void LoadPlayeModel()
    {
        if (this._playerModel != null) return;
        this._playerModel = transform.Find("PlayerModel").transform;
        Debug.LogWarning(transform.name + ": LoadPlayerMovement", gameObject);
    }

    private void LoadPlayerMovement()
    {
        if (this._playerMovement != null) return;
        this._playerMovement = transform.GetComponentInChildren<PlayerMovement>();
        Debug.LogWarning(transform.name + ": LoadPlayerMovement", gameObject);
    }

    private void LoadPlayerStats()
    {
        if (this._playerStats != null) return;
        this._playerStats = transform.GetComponentInChildren<PlayerStats>();
        Debug.LogWarning(transform.name + ": LoadPlayerStats", gameObject);
    }

    private void LoadPlayerSkills()
    {
        if (this._playerSkills != null) return;
        this._playerSkills = transform.GetComponentInChildren<PlayerSkills>();
        Debug.LogWarning(transform.name + ": LoadPlayerSkills", gameObject);
    }

    private void LoadPlayerLevel()
    {
        if (this._playerLevel != null) return;
        this._playerLevel = transform.GetComponentInChildren<PlayerLevel>();
        Debug.LogWarning(transform.name + ": LoadPlayerLevel", gameObject);
    }

    private void LoadPlayerInventory()
    {
        if (this._playerInventory != null) return;
        this._playerInventory = transform.GetComponentInChildren<PlayerInventory>();
        Debug.LogWarning(transform.name + ": LoadPlayerInventory", gameObject);
    }

    private void LoadPlayerEquipInv()
    {
        if (this._playerEquipInv != null) return;
        this._playerEquipInv = transform.GetComponentInChildren<PlayerEquipInv>();
        Debug.LogWarning(transform.name + ": LoadPlayerEquipInv", gameObject);
    }

    private void LoadPlayerAnimation()
    {
        if (this._playerAnimation != null) return;
        this._playerAnimation = transform.GetComponentInChildren<PlayerAnimation>();
        Debug.LogWarning(transform.name + ": LoadPlayerAnimation", gameObject);
    }
    private void LoadPlayerDead()
    {
        if (this._playerDead != null) return;
        this._playerDead = transform.GetComponentInChildren<PlayerDead>();
        Debug.LogWarning(transform.name + ": LoadPlayerDead", gameObject);
    }

    private void LoadRigidbody2D()
    {
        if (this._rb != null) return;
        this._rb = transform.GetComponent<Rigidbody2D>();
        Debug.LogWarning(transform.name + ": LoadRigidbody2D", gameObject);
    }
    private void LoadCollider2D()
    {
        if (this._cc != null) return;
        this._cc = transform.GetComponent<CapsuleCollider2D>();
        Debug.LogWarning(transform.name + ": LoadCollider2D", gameObject);
    }
}
