using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIExpBarCtrl : SecondMonoBehaviour
{
    private static UIExpBarCtrl _instance;
    public static UIExpBarCtrl instance => _instance;

    [SerializeField] private Slider _EXPSlider;
    [SerializeField] private TextMeshProUGUI _EXPText;
    [SerializeField] private TextMeshProUGUI _LevelText;

    [SerializeField] private ExpBarManager _ExpBarManager;

    protected override void Awake()
    {
        base.Awake();
        if (UIExpBarCtrl._instance != null) Debug.LogError("Only 1 UIExpBarCtrl allow to exist");
        UIExpBarCtrl._instance = this;
    }

/*    protected override void Start()
    {
        base.Start();
*//*        this.SetNamePlayer();
        this.SetLevelPlayer();*//*
        this.SetHPMPBarPlayer();*//*
    }*/

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEXPSlider();
        this.LoadEXPText();
        this.LoadLevelText();
        this.LoadExpBarManager();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this._ExpBarManager.OnExpBarChange += SetExpBar;
    }

    private void LoadEXPSlider()
    {
        if (this._EXPSlider != null) return;
        this._EXPSlider = transform.GetComponentInChildren<Slider>();
        Debug.LogWarning(transform.name + ": LoadEXPSlider", gameObject);
    }

    private void LoadEXPText()
    {
        if (this._EXPText != null) return;
        this._EXPText = transform.Find("ExpText").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadEXPText", gameObject);
    }
    private void LoadLevelText()
    {
        if (this._LevelText != null) return;
        this._LevelText = transform.parent.Find("HpMpBar").Find("LevelText").GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadLevelText", gameObject);
    }

    private void LoadExpBarManager()
    {
        if (this._ExpBarManager != null) return;
        this._ExpBarManager = transform.GetComponent<ExpBarManager>();
        Debug.LogWarning(transform.name + ": LoadExpBarManager", gameObject);
    }

    private void SetExpBar()
    {
        int currentEXP = PlayerLevel.Instance.CurrentExperience;
        int maxEXP = PlayerLevel.Instance.MaxExperience;
        int level = PlayerLevel.Instance.CurrentLevel;

        float value = Mathf.Clamp01((float)currentEXP / (float)maxEXP);
        this._EXPSlider.value = value;
        this._EXPText.SetText("EXP: " + currentEXP + "/" + maxEXP + " [" + value * 100 + "%]");
        this._LevelText.SetText("Lv." + level);

        //UIHPMPBarCtrl.instance.SetLevelPlayer(level);
    }
    /*    private void SetLevelPlayer()
        {
            int level = PlayerStats.Instance.level;
            this.levelText.SetText("Lv. " + level);
        }

        private void SetNamePlayer()
        {
            string name = PlayerStats.Instance.name;
            this.nameText.SetText(name);
        }*/
}
