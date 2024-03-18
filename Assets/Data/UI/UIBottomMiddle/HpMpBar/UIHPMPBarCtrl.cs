using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHPMPBarCtrl : SecondMonoBehaviour
{
    private static UIHPMPBarCtrl _instance;
    public static UIHPMPBarCtrl instance => _instance;

    [SerializeField] private Slider _HPSlider;
    [SerializeField] private Slider MPSlider;
    [SerializeField] private TextMeshProUGUI _HPText;
    [SerializeField] private TextMeshProUGUI _MPText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _nameText;

    [SerializeField] private HPMPBarManager _HPMPBarManager;

    protected override void Awake()
    {
        base.Awake();
        if (UIHPMPBarCtrl._instance != null) Debug.LogError("Only 1 UIHPMPBarCtrl allow to exist");
        UIHPMPBarCtrl._instance = this;
    }

    protected override void Start()
    {
        base.Start();

        this.SetLevelPlayer(PlayerLevel.Instance.CurrentLevel);
        this.SetNamePlayer();
        this.SetHPMPBarPlayer();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadHPSlider();
        this.LoadMPSlider();
        this.LoadHPText();
        this.LoadMPText();
        this.LoadLevelText();
        this.LoadNameText();
        this.LoadHPMPBarManager();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        this._HPMPBarManager.OnHPMPBarChange += SetHPMPBarPlayer;
        PlayerLevel.Instance.ExperienceManager.OnLevelChange += SetLevelPlayer;

    }
    private void LoadHPSlider()
    {
        if (this._HPSlider != null) return;
        this._HPSlider = transform.Find("HPSlider").GetComponent<Slider>();
        Debug.LogWarning(transform.name + ": LoadHPSlider", gameObject);
    }

    private void LoadMPSlider()
    {
        if (this.MPSlider != null) return;
        this.MPSlider = transform.Find("MPSlider").GetComponent<Slider>();
        Debug.LogWarning(transform.name + ": LoadMPSlider", gameObject);
    }

    private void LoadHPText()
    {
        if (this._levelText != null) return;
        this._HPText = transform.Find("HPSlider").GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadLevelText", gameObject);
    }
    private void LoadMPText()
    {
        if (this._nameText != null) return;
        this._MPText = transform.Find("MPSlider").GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadNameText", gameObject);
    }

    private void LoadLevelText()
    {
        if (this._levelText != null) return;
        this._levelText = transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadLevelText", gameObject);
    }
    private void LoadNameText()
    {
        if (this._nameText != null) return;
        this._nameText = transform.Find("NameText").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadNameText", gameObject);
    }
    private void LoadHPMPBarManager()
    {
        if (this._HPMPBarManager != null) return;
        this._HPMPBarManager = transform.GetComponent<HPMPBarManager>();
        Debug.LogWarning(transform.name + ": LoadHPMPBarManager", gameObject);
    }

    private void SetHPMPBarPlayer()
    {
        int currentHP = PlayerStats.Instance.currentHP;
        int totalHP = PlayerStats.Instance.MaxHp + PlayerStats.Instance.EtcMaxHP + PlayerStats.Instance.SkillMaxHP;
        int currentMP = PlayerStats.Instance.currentMP;
        int totalMP = PlayerStats.Instance.MaxMp + PlayerStats.Instance.EtcMaxMP;

        this._HPSlider.value = Mathf.Clamp01((float)currentHP / (float)totalHP);
        this.MPSlider.value = Mathf.Clamp01((float)currentMP / (float)totalMP);
        this._HPText.SetText(currentHP + "/" + totalHP);
        this._MPText.SetText(currentMP + "/" + totalMP);
    }
    public void SetLevelPlayer(int level)
    {
        this._levelText.SetText("Lv. " + level);
    }

    private void SetNamePlayer()
    {
        string namePlayer = PlayerStats.Instance.playerName; 
        this._nameText.SetText(namePlayer);
    }
}
