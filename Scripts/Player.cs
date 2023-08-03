using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _checkGroundRadius;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundCheck;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveVector;
    private int _coin;
    private readonly int _move = Animator.StringToHash("Speed");
    private readonly int _ground = Animator.StringToHash("IsGrounded");

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _checkGroundRadius = _groundCheck.GetComponent<CircleCollider2D>().radius;
    }

    private void Update()
    {
        Run();
        Flip();
        DrawAnimation();

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, _checkGroundRadius, _groundMask);
    }

    private void Flip()
    {
        _spriteRenderer.flipX = _moveVector.x < 0;
    }

    private void Run()
    {
        _moveVector.x = Input.GetAxis("Horizontal");
        _rigidbody.velocity = new Vector2(_moveVector.x * _speed, _rigidbody.velocity.y);
    }

    private void DrawAnimation()
    {
        _animator.SetFloat(_move, Mathf.Abs(_moveVector.x));
        _animator.SetBool(_ground, IsGrounded());
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<AppleCoin>(out AppleCoin appleCoin))
        {
            _coin++;
        }
    }
}
