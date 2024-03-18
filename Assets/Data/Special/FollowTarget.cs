using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _offsetX = 0f;
    [SerializeField] private float _offsetY = 0f;
    [SerializeField] private float _offsetZ = 0f;

    protected virtual void FixedUpdate()
    {
        this.Following();
    }

    private void Following()
    {
        if (this._target == null) return;
        Vector3 targetPos = new Vector3(_target.position.x + _offsetX, _target.position.y + _offsetY, _target.position.y + _offsetZ);
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime * this._speed);
    }
}
