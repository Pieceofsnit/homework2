using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _turningPointA;
    [SerializeField] private Transform _turningPointB;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    private Vector2 _moveVector;
    private int _direction = 1;

    private void Start()
    {
        _moveVector.x = _direction;
    }

    private void Update()
    {
        Patrol();
        Flip();
    }

    private void Patrol()
    {
        _animator.SetFloat("Move", Mathf.Abs(_moveVector.x));

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
