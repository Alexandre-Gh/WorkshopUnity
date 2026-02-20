using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BoxCollider2D _hitbox;

    [SerializeField] private float _speed = 200;

    [SerializeField] private Animator _animatorController;

    private Vector2 _direction = Vector2.left;

    [SerializeField] private LayerMask _wallLayer;


    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _hitbox = gameObject.GetComponent<BoxCollider2D>();
        _animatorController = gameObject.GetComponent<Animator>();
    }

    void HandleDirection()
    {
        if (_rb.velocity.x != 0)
        {
            if (_rb.velocity.x > 0)
            {
                transform.localScale = Vector3.one;
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            
        }
    }

    void CheckTurnAround()
    {
        bool value = Physics2D.Raycast(_hitbox.bounds.center, _direction, _hitbox.bounds.size.x / 2 + 0.015f, _wallLayer).collider != null;

        if (value)
        {
            if (_direction == Vector2.left)
            {
                _direction = Vector2.right;
            }
            else
            {
                _direction = Vector2.left;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleDirection();
        

        _animatorController.SetFloat("VelocityX", Mathf.Abs(_rb.velocity.x));
    }

    void FixedUpdate()
    {
        CheckTurnAround();
        _rb.velocity = new Vector2(_speed * Time.fixedDeltaTime * _direction.x, _rb.velocity.y);
    }
}
