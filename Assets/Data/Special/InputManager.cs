using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : SecondMonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance => _instance;

    [SerializeField] private KeyBindings _keyBindings;
    public KeyBindings KeyBindings => _keyBindings;

    [SerializeField] private Vector3 _mouseWorldPos;
    public Vector3 MouseWorldPos => _mouseWorldPos;

    [SerializeField] private Vector4 _direction;
    public Vector4 Direction => _direction;

    protected override void Awake()
    {
        if (InputManager._instance != null) Debug.LogError("Only 1 InputManager allow to exist");
        InputManager._instance = this;
    }

    private void FixedUpdate()
    {
        this.SetMousePos();
    }

    private void SetMousePos()
    {
        this._mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this._mouseWorldPos.z = 0;
    }

    private void Update()
    {
        this.GetkDirection();

        if (InputManager.Instance.GetKeyDown(KeybindingActions.Stats))
        {
            UIPlayerStatsCtrl.Instance.Toggle();
        }

        if (InputManager.Instance.GetKeyDown(KeybindingActions.Skills))
        {
            UIPlayerSkillCtrl.Instance.Toggle();
        }

        if (InputManager.Instance.GetKeyDown(KeybindingActions.Equipment))
        {
            UIPlayerEquipmentInventoryCtrl.Instance.Toggle();
        }

        if (InputManager.Instance.GetKeyDown(KeybindingActions.Items))
        {
            UIInventoryCtrl.Instance.Toggle();
        }

        if (InputManager.Instance.GetKeyDown(KeybindingActions.KeyBindings))
        {
            UIKeyBindingsCtrl.Instance.KeyBindingsToggle();
        }
    }

    private void GetkDirection()
    {
        this._direction.x = Input.GetKey(KeyCode.LeftArrow) ? 1 : 0; 
        this._direction.y = Input.GetKey(KeyCode.RightArrow) ? 1 : 0; 
        this._direction.z = Input.GetKey(KeyCode.UpArrow) ? 1 : 0; 
        this._direction.w = Input.GetKey(KeyCode.DownArrow) ? 1 : 0;
    }

    public virtual void AddKeyBindings(KeyBindingCheck key)
    {
        foreach (KeyBindingCheck keyBindingCheck in this._keyBindings.keyBindingChecks)
        {
            if (key.keybindingActions == keyBindingCheck.keybindingActions ) return;    // If the added key duplicates an existing key, do not add it
        }
        this._keyBindings.keyBindingChecks.Add(key);
    }

    public virtual void ClearKeyBindings()
    {
        this._keyBindings.keyBindingChecks.Clear();
    }

    public bool GetKeyDown(KeybindingActions key)
    {
        foreach (KeyBindingCheck keyBindingCheck in _keyBindings.keyBindingChecks)
        {
            if (keyBindingCheck.keybindingActions == key)
            {
                return Input.GetKeyDown(keyBindingCheck.keyCode);
            }
        }
        return false;
    }

    public bool GetKey(KeybindingActions key)
    {
        foreach (KeyBindingCheck keyBindingCheck in _keyBindings.keyBindingChecks)
        {
            if (keyBindingCheck.keybindingActions == key)
            {
                return Input.GetKey(keyBindingCheck.keyCode);
            }
        }
        return false;
    }

    public bool GetKeyUp(KeybindingActions key)
    {
        foreach (KeyBindingCheck keyBindingCheck in _keyBindings.keyBindingChecks)
        {
            if (keyBindingCheck.keybindingActions == key)
            {
                return Input.GetKeyUp(keyBindingCheck.keyCode);
            }
        }
        return false;
    }
}