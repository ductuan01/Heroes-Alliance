using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : PlayerAbstract
{
    private static PlayerAnimation _instance;
    public static PlayerAnimation Instance => _instance;

    [SerializeField] private Animator _playerAnim;

    [SerializeField] private SpriteRenderer _sprite;

    private static readonly int _Idle = Animator.StringToHash("Knight_Idle");
    private static readonly int _Walk = Animator.StringToHash("Knight_Walk");
    private static readonly int _Attack = Animator.StringToHash("Knight_Attack");
    private static readonly int _Jumping = Animator.StringToHash("Knight_Jump");
    private static int _currentState;
    private static float _lockedTill = 0.1f;

    private Color32 originalColor = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
    private bool isBlinking = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAnimator();
    }
    private void LoadAnimator()
    {
        if (this._playerAnim != null) return;
        this._playerAnim = transform.GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
    }

    protected override void Awake()
    {
        if (PlayerAnimation._instance != null) Debug.LogError("Only 1 PlayerAnimation allow to exist");
        PlayerAnimation._instance = this;
    }

    protected override void Start()
    {
        base.Start();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        isBlinking = false;
        _sprite.color = originalColor;
    }

    private void Update()
    {
        var state = GetState();
        if (state == _currentState) return;
        this._playerAnim.CrossFade(state, 0.1f, 0);
        _currentState = state;

        UpdateAnimationSpeed();
    }
    void UpdateAnimationSpeed()
    {
        _playerAnim.SetFloat("Speed", PlayerCtrl.PlayerStats.AtttackSpeed / 8f);
    }

    private int GetState()
    {
        if (Time.time < _lockedTill) return _currentState;

        if (PlayerCtrl.PlayerSkills.IsAttacking) return LockState(_Attack, PlayerCtrl.PlayerStats.AtttackSpeed / 16f);
        if (PlayerCtrl.PlayerMovement.IsJumping) return _Jumping;
        if (PlayerCtrl.PlayerMovement.IsGrounded) return (InputManager.Instance.Direction.x == 0 && InputManager.Instance.Direction.y == 0) ? _Idle : _Walk;
        return PlayerCtrl.PlayerMovement.IsJumping == true ? _Jumping : _Walk;

        static int LockState(int s, float t)
        {
            _lockedTill = Time.time + t;
            return s;
        }
    }

    public void BlinkBlink()
    {
        if (!isBlinking && !PlayerCtrl.PlayerStats.IsDead)
        {
            isBlinking = true;
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        while (isBlinking)
        {
            for (int i = 0; i < 3; i++)
            {
                _sprite.color = new Color32(0xFF, 0x64, 0x64, 0xFF);
                yield return new WaitForSeconds(0.1f);
                _sprite.color = originalColor;
                yield return new WaitForSeconds(0.1f);
            }
            isBlinking = false;
        }
    }
}
