using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsCtrl : SecondMonoBehaviour
{
    [SerializeField] private List<PlayerStat> _playerStats;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerStats();
    }

    protected virtual void LoadPlayerStats()
    {
        if (this._playerStats.Count > 0) return;
        foreach(Transform playerStat in this.transform)
        {
            PlayerStat statComponent = playerStat.GetComponent<PlayerStat>();
            if (statComponent != null)
            {
                this._playerStats.Add(statComponent);
            }
        }
    }

    public virtual List<PlayerStat> GetPlayerStats()
    {
        return this._playerStats;
    }
}
