using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSkillsAbstract : SecondMonoBehaviour
{
    [Header("Player Abstract")]
    [SerializeField] private PlayerSkills _playerSkills;
    public PlayerSkills PlayerSkills => _playerSkills;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerSkills();
    }

    protected virtual void LoadPlayerSkills()
    {
        if (_playerSkills != null) return;
        this._playerSkills = transform.parent.GetComponentInParent<PlayerSkills>();
        Debug.LogWarning(transform.name + ": LoadPlayerSkills", gameObject);
    }
}
