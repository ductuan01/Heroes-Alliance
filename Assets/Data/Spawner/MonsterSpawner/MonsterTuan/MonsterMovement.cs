using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonsterAbstract
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private float _wallCheckRadius;
    [SerializeField] private float _abyssCheckRadius;
    [SerializeField] private float _slopeCheckDistance;
    [SerializeField] private float _maxSlopeAngle;
    
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private Transform _abyssCheck;

    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask _whatIsWall;

    [SerializeField] private bool _bIsGoingRight = true;
    [SerializeField] public bool _bIsChaseMode = false;
    [SerializeField] public bool _bIsStop = false;

    [SerializeField] private PhysicsMaterial2D _noFriction;
    [SerializeField] private PhysicsMaterial2D _fullFriction;

    [SerializeField] private float _xInput = 1;
    public float xInput => _xInput;

    private float _slopeDownAngle;
    private float _slopeSideAngle;
    private float _lastSlopeAngle;

    [SerializeField] private bool _isWall;
    [SerializeField] private bool _isAbyss;
    [SerializeField] private bool _isGrounded;
    [SerializeField] public bool IsGrounded => _isGrounded;
    [SerializeField] private bool _isOnSlope;
    [SerializeField] private bool _isJumping;
    [SerializeField] private bool _canWalkOnSlope;

    private Vector2 _newVelocity;
    private Vector2 _capsuleColliderSize;
    private Vector2 _slopeNormalPerp;

    private Rigidbody2D _rb;
    private CapsuleCollider2D _cc;

    [SerializeField] private float _chaseTimer = 0.0f;
    private float _chaseDuration = 10f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTransform();
    }

    private void LoadTransform()
    {
        _groundCheck = transform.Find("GroundCheck").transform;
        _wallCheck = transform.Find("WallCheck").transform;
        _abyssCheck = transform.Find("AbyssCheck").transform;

        _whatIsGround = LayerMask.GetMask("Ground") | LayerMask.GetMask("PlatformETC");
        _whatIsWall = LayerMask.GetMask("WallETC") | LayerMask.GetMask("PlatformETC");
    }

    protected override void Start()
    {
        _rb = transform.parent.GetComponent<Rigidbody2D>();
        _cc = transform.parent.GetComponent<CapsuleCollider2D>();
        _capsuleColliderSize = _cc.size;

        StartCoroutine(PerformAttack());
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        this._bIsChaseMode = false;
        this._bIsStop = false;
    }

    private void FixedUpdate()
    {
        if (PlayerStats.Instance.IsDead) this._bIsChaseMode = false;
        if (this._bIsChaseMode)
        {
            this._chaseTimer += Time.deltaTime;
            if (this._chaseTimer >= this._chaseDuration)
            {
                this._bIsChaseMode = false;
            }
        }

        this.CheckInput();
        this.CheckGround();
        this.CheckWall();
        this.CheckAbyss();
        this.SlopeCheck();
        this.ApplyMovement();
    }

    private void CheckInput()
    {
        if (MonsterCtrl.MonsterStats.isDead)
        {
            this._xInput = 0;
            this._bIsStop = true;
            this._bIsChaseMode = false;
        }
        else
        {
            if (!this._bIsChaseMode)
            {
                if (this._bIsStop)
                {
                    this._xInput = 0;
                }
                if (!this._bIsStop)
                {
                    this._xInput = (_bIsGoingRight) ? 1 : -1;
                }
            }
            if (this._bIsChaseMode)
            {
                if (transform.parent.position.x + 1 < PlayerCtrl.Instance.transform.position.x)
                {
                    _xInput = 1;
                    if (!this._bIsGoingRight) this.Flip();
                }
                else if (transform.parent.position.x - 1 > PlayerCtrl.Instance.transform.position.x)
                {
                    _xInput = -1;
                    if (this._bIsGoingRight) this.Flip();
                }
                else
                {
                    _xInput = 0;
                }
            }
        }
    }    

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _whatIsGround);
    }

    private void CheckWall()
    {
        this._isWall = Physics2D.OverlapCircle(_wallCheck.position, _wallCheckRadius, _whatIsWall);
        if (_isWall)
        {
            this.Flip();
            this._bIsChaseMode = false;
        }
    }

    private void CheckAbyss()
    {
        this._isAbyss = !Physics2D.OverlapCircle(_abyssCheck.position, _abyssCheckRadius, _whatIsGround);
        if (_isAbyss && _rb.velocity.y >= 0.0f)
        {
            this.Flip();
            this._bIsChaseMode = false;
        }
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, _capsuleColliderSize.y / 2));

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
            _rb.sharedMaterial = _fullFriction;
        }
        else
        {
            _rb.sharedMaterial = _noFriction;
        }
    }
    private void ApplyMovement()
    {
        if (_isGrounded && !_isOnSlope && !_isJumping)
        {
            _newVelocity.Set(_movementSpeed * this._xInput, 0.0f);
            _rb.velocity = _newVelocity;
        }
        if (_isGrounded && _isOnSlope && _canWalkOnSlope && !_isJumping)
        {
            _newVelocity.Set(_movementSpeed * _slopeNormalPerp.x * -this._xInput, _movementSpeed * _slopeNormalPerp.y * -this._xInput);
            _rb.velocity = _newVelocity;
        }
    }    

    public void Flip()
    {
        _bIsGoingRight = !_bIsGoingRight;
        transform.parent.Rotate(0.0f, 180.0f, 0.0f);
        MonsterCtrl._HPBar.transform.Rotate(0.0f, 180.0f, 0.0f);
        MonsterCtrl._monsterName.transform.parent.parent.Rotate(0.0f, 180.0f, 0.0f);
    }

    private IEnumerator PerformAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 6f));

            int randomStop = Random.Range(0, 5);
            int randomFlip = Random.Range(0, 5);
            if (randomStop == 1)
            {
                this._bIsStop = true;
            }
            else
            {
                this._bIsStop = false;
                if(randomFlip == 1)
                {
                    this.Flip();
                }    
            }
        }
    }
    public void SetChaseActive()
    {
        this._bIsChaseMode = true;
        this._chaseTimer = 0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _groundCheckRadius);
        Gizmos.DrawWireSphere(_wallCheck.position, _wallCheckRadius);
        Gizmos.DrawWireSphere(_abyssCheck.position, _abyssCheckRadius);
    }
}
