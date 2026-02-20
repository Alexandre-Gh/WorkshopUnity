using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 200;
    [SerializeField] private float _jumpSpeed = 5;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private BoxCollider2D _hitbox;

    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private LayerMask _enemyLayer;

    [SerializeField] private GameObject _cameraObject;

    [SerializeField] private Animator _animatorController;

    [SerializeField] private Vector3 _initialPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _hitbox = gameObject.GetComponent<BoxCollider2D>();
        _animatorController = gameObject.GetComponent<Animator>();

        gameObject.transform.position = _initialPosition;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == _enemyLayer)
        {
            gameObject.transform.position = _initialPosition;
        }
    }

    public void Respawn()
    {
        gameObject.transform.position = _initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        HandleCamera();
        HandleDirection();

        _animatorController.SetFloat("VelocityX", Mathf.Abs(_rb.velocity.x));
        _animatorController.SetFloat("VelocityY", Mathf.Abs(_rb.velocity.y));
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

    bool CanJump()
    {
        bool raycast = Physics2D.BoxCast(_hitbox.bounds.center, _hitbox.bounds.size, 0, Vector2.down, 0.015f, _wallLayer).collider != null;
        return raycast;
    }

    void HandleCamera()
    {
        if (_cameraObject != null)
        {
            _cameraObject.transform.position = 
                new Vector3(transform.position.x, transform.position.y, -10);
        }
    }

    void FixedUpdate()
    {
        Vector2 nextVelocity = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            nextVelocity.x = _speed * -1 * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            nextVelocity.x = _speed * Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.UpArrow) && CanJump())
        {
            nextVelocity.y = _jumpSpeed;
        } else
        {
            nextVelocity.y = _rb.velocity.y;
        }
        _rb.velocity = new Vector2(nextVelocity.x, nextVelocity.y);
    }
}
