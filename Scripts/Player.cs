using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _checkGroundRadius;
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private Transform GroundCheck;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveVector;
    private int _coin;
    private bool _isGrounded;
    private readonly int _move = Animator.StringToHash("Speed");
    private readonly int _ground = Animator.StringToHash("IsGrounded");


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _checkGroundRadius = GroundCheck.GetComponent<CircleCollider2D>().radius;
    }

    private void Update()
    {
        Run();
        Flip();
        CheckingGround();
        Jump();
        DrawAnimation();
        InputMove();
    }

    private void Flip()
    {
        _spriteRenderer.flipX = _moveVector.x < 0;
    }

    private void Run()
    {
        _rigidbody.velocity = new Vector2(_moveVector.x * _speed, _rigidbody.velocity.y);
    }

    private void DrawAnimation()
    {
        _animator.SetFloat(_move, Mathf.Abs(_moveVector.x));
        _animator.SetBool(_ground, _isGrounded);
    }

    private void InputMove()
    {
        _moveVector.x = Input.GetAxisRaw("Horizontal");
    }

    private void Jump()
    {
        if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
        }
    }

    private void CheckingGround()
    {
        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, _checkGroundRadius, Ground);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<AppleCoin>(out AppleCoin appleCoin))
        {
            _coin++;
        }
    }
}
