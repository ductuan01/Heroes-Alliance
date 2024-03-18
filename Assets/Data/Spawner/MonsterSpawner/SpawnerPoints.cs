using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerPoints : SecondMonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPoints();
    }

    private void LoadPoints()
    {
        if (this._points.Count > 0) return;
        foreach(Transform point in transform)
        {
            this._points.Add(point);
        }
        Debug.LogWarning(transform.name + ": LoadPoints", gameObject);
    }

    public Transform GetRandomPoint()
    {
        int rand = Random.Range(0, this._points.Count);
        return this._points[rand];
    }
}
