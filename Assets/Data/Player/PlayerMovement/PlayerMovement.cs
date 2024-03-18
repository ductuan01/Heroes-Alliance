using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerAbstract
{
    [SerializeField] private RopeClimb _ropeClimb;
    public RopeClimb RopeClimb => _ropeClimb;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _slopeCheckDistance;
    [SerializeField] private float _maxSlopeAngle;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;

    [SerializeField] private PhysicsMaterial2D _noFriction;
    [SerializeField] private PhysicsMaterial2D _fullFriction;

    private float _xInput;

    private float _slopeDownAngle;
    private float _slopeSideAngle;
    private float _lastSlopeAngle;

    private int _facingDirection = 1;
    public int FacingDirection => _facingDirection;

    private bool _isGrounded;
    public bool IsGrounded => _isGrounded;

    private bool _isJumping;
    public bool IsJumping => _isJumping;

    private bool _isOnSlope;

    private bool _canWalkOnSlope;
    private bool _canJump;

    private Vector2 _newVelocity;
    private Vector2 _newForce;
    private Vector2 _slopeNormalPerp;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRopeClimb();
    }

    private void LoadRopeClimb()
    {
        if (this._ropeClimb != null) return;
        this._ropeClimb = transform.GetComponentInChildren<RopeClimb>();
        Debug.LogWarning(transform.name + ": LoadRopeClimb", gameObject);
    }

    private void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        CheckGround();
        SlopeCheck();
        ApplyMovement();
    }

    private void CheckInput()
    {
        _xInput = 0f;
        if (PlayerCtrl.PlayerStats.IsDead) return;
        if (PlayerCtrl.PlayerSkills.IsAttacking) return;
        if (InputManager.Instance.Direction.x == 1)
        {
            _xInput = -1;
        }
        if (InputManager.Instance.Direction.y == 1)
        {
            _xInput = 1;
        }
        if (InputManager.Instance.Direction.x == 1 && InputManager.Instance.Direction.y == 1)
        {
            _xInput = 0;
        }
        if (_xInput == 1 && _facingDirection == -1)
        {
            Flip();
        }
        if (_xInput == -1 && _facingDirection == 1)
        {
            Flip();
        }

        if(InputManager.Instance.GetKey(KeybindingActions.Jump) && !Input.GetKey(KeyCode.DownArrow))
        {
            if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) if (this._ropeClimb.ExitClimping()) this._canJump = true;
            Jump();
        }
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _whatIsGround);
        
        if (PlayerCtrl.Rb.velocity.y <= 0.0f)
        {
            _isJumping = false;
        }
        if (_isGrounded && !_isJumping && _slopeDownAngle <= _maxSlopeAngle)
        {
            _canJump = true;
        }
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, PlayerCtrl.Cc.size.y / 2));
        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, _slopeCheckDistance, _whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, _slopeCheckDistance, _whatIsGround);
        if (slopeHitFront)
        {
            _isOnSlope = true;

            _slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack)
        {
            _isOnSlope = true;

            _slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            _slopeSideAngle = 0.0f;
            _isOnSlope = false;
        }

    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, _slopeCheckDistance, _whatIsGround);
        if (hit)
        {
            _slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            _slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (_slopeDownAngle != _lastSlopeAngle)
            {
                _isOnSlope = true;
            }

            _lastSlopeAngle = _slopeDownAngle;

            Debug.DrawRay(hit.point, _slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);

        }

        if (_slopeDownAngle > _maxSlopeAngle || _slopeSideAngle > _maxSlopeAngle)
        {
            _canWalkOnSlope = false;
        }
        else
        {
            _canWalkOnSlope = true;
        }

        if (_isOnSlope && _canWalkOnSlope && _xInput == 0.0f)
        {
            PlayerCtrl.Rb.sharedMaterial = _fullFriction;
        }
        else
        {
            PlayerCtrl.Rb.sharedMaterial = _noFriction;
        }
    }

    private void Jump()
    {
        if (_canJump && !this._ropeClimb.IsClimping)
        {
            _canJump = false;
            _isJumping = true;
            _newVelocity.Set(0.0f, 0.0f);
            PlayerCtrl.Rb.velocity = _newVelocity;
            _newForce.Set(0.0f, _jumpForce);
            PlayerCtrl.Rb.AddForce(_newForce, ForceMode2D.Impulse);
        }
    }

    private void ApplyMovement()
    {
        if (this._ropeClimb.IsClimping) return;
        if (_isGrounded && !_isOnSlope && !_isJumping) //if not on slope
        {
            _newVelocity.Set(_movementSpeed * _xInput, 0.0f);
            PlayerCtrl.Rb.velocity = _newVelocity;
        }
        else if (_isGrounded && _isOnSlope && _canWalkOnSlope && !_isJumping) //If on slope
        {
            _newVelocity.Set(_movementSpeed * _slopeNormalPerp.x * -_xInput, _movementSpeed * _slopeNormalPerp.y * -_xInput);
            PlayerCtrl.Rb.velocity = _newVelocity;
        }
        else if (!_isGrounded) //If in air
        {
            _newVelocity.Set(_movementSpeed * _xInput, PlayerCtrl.Rb.velocity.y);
            PlayerCtrl.Rb.velocity = _newVelocity;
        }
    }

    private void Flip()
    {
        _facingDirection *= -1;
        transform.parent.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
    }
}

