using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTutorial : SecondMonoBehaviour
{
    private static MonsterTutorial _instance;
    public static MonsterTutorial Instance => _instance;

    [SerializeField] private List<Transform> _monstersTutorial;

    protected override void Awake()
    {
        base.Awake();
        if (MonsterTutorial._instance != null) Debug.LogError("Only 1 MonsterTutorial allow to exist");
        MonsterTutorial._instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMonstersTurotial();
    }

    private void LoadMonstersTurotial()
    {
        if (this._monstersTutorial.Count > 1) return;
        foreach(Transform monster in this.transform)
        {
            this._monstersTutorial.Add(monster);
        }
        Debug.LogWarning(transform.name + ": LoadMonstersTurotial", gameObject);
    }    

    public void SetMonsterTutorialActive()
    {
        foreach (Transform monster in this._monstersTutorial)
        {
            monster.gameObject.SetActive(true);
        }
    }
}
