using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _checkRadius;
    private int _coin;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveVector;
    public bool _isGrounded;
    public Transform GroundCheck;
    public LayerMask Ground;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _checkRadius = GroundCheck.GetComponent<CircleCollider2D>().radius;
    }

    private void Update()
    {
        Run();
        Flip();
        CheckingGround();
        Jump();
    }

    private void Flip()
    {
        _spriteRenderer.flipX = _moveVector.x < 0;
    }

    private void Run()
    {
        _moveVector.x = Input.GetAxisRaw("Horizontal");
        _rigidbody.velocity = new Vector2(_moveVector.x * _speed, _rigidbody.velocity.y);
        _animator.SetFloat("Speed", Mathf.Abs(_moveVector.x));
    }

    private void Jump()
    {
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
        }
    }

    public void CheckingGround()
    {
        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, _checkRadius, Ground);
        _animator.SetBool("IsGrounded", _isGrounded);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<AppleCoin>(out AppleCoin appleCoin))
        {
            _coin++;
        }
    }
}
