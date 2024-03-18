using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStat : SecondMonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadText();
    }

    protected virtual void LoadText()
    {
        if (this._text != null) return;
        this._text = transform.GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + "LoadText", gameObject);
    }

    public virtual void SetText(string text)
    {
        this._text.SetText(text);
    }

    public virtual void SetPlayerStats(float BaseStat, float EtcStat)
    {
        this._text.SetText((BaseStat + EtcStat) + " (" + BaseStat + "+" + EtcStat + ")");
    }
}
