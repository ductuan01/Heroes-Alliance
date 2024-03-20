using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : PlayerAbstract
{
    private static PlayerLevel _instance;
    public static PlayerLevel Instance => _instance;

    [SerializeField] private int _currentLevel = 1;
    public int CurrentLevel => _currentLevel;

    [SerializeField] private int _currentExperience = 0;
    public int CurrentExperience => _currentExperience;

    [SerializeField] private int _maxExperience = 100;
    public int MaxExperience => _maxExperience;

    [SerializeField] private ExperienceManager _experienceManager;
    public ExperienceManager ExperienceManager => _experienceManager;

    protected override void Awake()
    {
        base.Awake();
        if (PlayerLevel._instance != null) Debug.LogError("Only 1 PlayerLevel allow to exist");
        PlayerLevel._instance = this;

        this.LoadLevelData();
    }

    protected override void Start()
    {
        base.Start();
        UIHPMPBarCtrl.instance.SetLevelPlayer(this._currentLevel);
        _experienceManager.AddExperience(0);
    }

    private void LoadLevelData()
    {
        string resPath = "GameData/PlayerLevel/LevelData";
        LevelDataSO levelDataSO = Resources.Load<LevelDataSO>(resPath);
        if (levelDataSO == null) return;
        this._currentLevel = levelDataSO.currentLevel;
        this._currentExperience = levelDataSO.currentExperience;
        this._maxExperience = levelDataSO.maxExperience;
    }

    public void SaveLevelData()
    {
        string resPath = "GameData/PlayerLevel/LevelData";
        LevelDataSO levelDataSO = Resources.Load<LevelDataSO>(resPath);
        if (levelDataSO == null) return;
        levelDataSO.currentLevel = this._currentLevel;
        levelDataSO.currentExperience = this._currentExperience;
        levelDataSO.maxExperience = this._maxExperience;
    }
    public void LevelDataNewGame()
    {
        string resPath = "GameData/PlayerLevel/LevelData";
        LevelDataSO levelDataSO = Resources.Load<LevelDataSO>(resPath);
        if (levelDataSO == null) return;
        levelDataSO.currentLevel = 1;
        levelDataSO.currentExperience = 0;
        levelDataSO.maxExperience = 100;
        this.LoadLevelData();
        ExpBarManager.instance.ExpBarChange();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadExperienceManager();
    }

    protected virtual void LoadExperienceManager()
    {
        if (this._experienceManager != null) return;
        this._experienceManager = transform.GetComponentInChildren<ExperienceManager>();
        Debug.Log(transform.name + ": LoadExperienceManager", gameObject);
    }

    protected override void OnEnable()
    {
        this._experienceManager.OnExperienceChange += HandleExperienceChange;
        Debug.Log("Subscribe event");
    }

    protected override void OnDisable()
    {
        this._experienceManager.OnExperienceChange -= HandleExperienceChange;
        Debug.Log("Unsubscribing event");
    }

    private void HandleExperienceChange(int newExperience)
    {
        this._currentExperience += newExperience;
        if (this._currentExperience >= _maxExperience)
        {
            this.LevelUp();
        }
        ExpBarManager.instance.ExpBarChange();
    }

    private void LevelUp()
    {
        this._currentLevel++;

        PlayerCtrl.PlayerStats.RaiseBaseAfterLevelUp();
        PlayerCtrl.PlayerSkills.AddSkillPointAfterLevelUp();
        ImprovingMaxHPIncrease.Instance.ImprovingMaxHPSkill();

        _currentExperience = 0;
        _maxExperience += 10;
        _experienceManager.LevelUp(this._currentLevel);

        Transform transform = LevelEffectSpawner.instance.Spawn(LevelEffectSpawner.levelEffect_1, this.transform.position, Quaternion.identity);
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        transform.gameObject.SetActive(true);
    }

    public void ReduceExpAfterDead()
    {
        int expReduce = _currentExperience * 10 / 100;
        Debug.Log(expReduce);
        ExperienceManager.Instance.AddExperience(-expReduce);
    }
}
