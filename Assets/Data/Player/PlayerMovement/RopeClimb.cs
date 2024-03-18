using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RopeClimb : SecondMonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private CapsuleCollider2D _cc;
    [SerializeField] public float climbSpeed = 0.5f;
    [SerializeField] private bool _isRope = false;
    [SerializeField] private bool _isClimping = false;
    public bool IsClimping => _isClimping;

    [SerializeField] private float vertical;

    [SerializeField] private Collider2D _collider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody2D();
        this.LoadCollider2D();
        this.LoadPlatform();
    }

    private void LoadRigidbody2D()
    {
        if (this._rb != null) return;
        this._rb = transform.parent.GetComponentInParent<Rigidbody2D>();
        Debug.LogWarning(transform.name + ": LoadRigidbody2D", gameObject);
    }
    private void LoadCollider2D()
    {
        if (this._cc != null) return;
        this._cc = transform.parent.GetComponentInParent<CapsuleCollider2D>();
        Debug.LogWarning(transform.name + ": LoadCollider2D", gameObject);
    }
    private void LoadPlatform()
    {
        if (this._collider != null) return;
        this._collider = GameObject.Find("Grid").transform.Find("Platform").GetComponent<Collider2D>();
        Debug.LogWarning(transform.name + ": LoadCollider2D", gameObject);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && _isRope)
        {
            _isClimping = true;
        }
        if(Input.GetKey(KeyCode.DownArrow) && _isRope)
        {
            _isClimping = true;
        }
        if (_isClimping)
        {
            _collider.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.UpArrow) && _isClimping && _isRope)
        {
            _rb.velocity = Vector2.up;
            _rb.AddForce(new Vector2(0f, climbSpeed), ForceMode2D.Impulse);
            
        }
        else if(Input.GetKey(KeyCode.DownArrow) && _isClimping && _isRope)
        {
            _rb.velocity = Vector2.down;
            _rb.AddForce(new Vector2(0f, -climbSpeed), ForceMode2D.Impulse);

        }
        else if (_isClimping && _isRope)
        {
            _rb.velocity = Vector2.zero;
            _rb.gravityScale = 0f;
        }
    }
    public bool ExitClimping()
    {
        if (!this._isClimping) return false;
        this._isClimping = false;
        return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Rope")
        {
            _isRope = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Rope")
        {
            _isRope = false;
            _isClimping = false;
            _collider.enabled = true;
            _rb.gravityScale = 1f;
        }
    }
}
