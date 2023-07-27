using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _turningPointA;
    [SerializeField] private Transform _turningPointB;
    [SerializeField] private float _speed;

    private Vector2 _moveVector;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private int _direction = 1;
    private readonly int _move = Animator.StringToHash("Move");

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _moveVector.x = _direction;
    }

    private void Update()
    {
        Patrol();
        Flip();
    }

    private void Patrol()
    {
        
        _animator.SetFloat(_move, Mathf.Abs(_moveVector.x));

        if (_turningPointA.position.x >= transform.position.x)
        {
            _moveVector.x = _direction;
        }
        else if (_turningPointB.position.x <= transform.position.x)
        {
            _moveVector.x = - _direction;
        }

        _rigidbody.velocity = new Vector2(_moveVector.x * _speed, _rigidbody.velocity.y);
    }

    private void Flip()
    {
        _spriteRenderer.flipX = _moveVector.x < 0;
    }
}
