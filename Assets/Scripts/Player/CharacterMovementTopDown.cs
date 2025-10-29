using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovementTopDown : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;

    private InputAction _moveAction;
    private Vector2 _moveDirection;
    private bool _isMoving = true;

    private int horizontalInput = Animator.StringToHash("x");
    private int verticalInput = Animator.StringToHash("y");

    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;

    public bool FacingLeft { get; private set; }
    public float MoveMagnitude { get; private set; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rb.gravityScale = 0;

        _anim = GetComponent<Animator>();
        _anim.SetFloat(verticalInput, -1f);

        _moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        _moveDirection = _moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            MoveMagnitude = _moveDirection.magnitude;
            _anim.SetFloat("magnitude", MoveMagnitude);

            if (MoveMagnitude != 0)
            {
                Vector2 animationDirection = GetAnimationDirection();
                _anim.SetFloat(horizontalInput, animationDirection.x);
                _anim.SetFloat(verticalInput, animationDirection.y);
            }

            if (_moveDirection.x < 0)
            {
                FacingLeft = true;
            }

            if (_moveDirection.x > 0)
            {
                FacingLeft = false;
            }

            Move();
        }
        else
        {
            MoveMagnitude = 0f;
            _rb.linearVelocity = Vector2.zero;
        }
    }

    private void Move()
    {
        if (_moveDirection.magnitude > 0)
        {
            _rb.AddForce(_moveDirection * _acceleration, ForceMode2D.Force);

            if (_rb.linearVelocity.magnitude > _maxSpeed)
            {
                _rb.linearVelocity = _rb.linearVelocity.normalized * _maxSpeed;
            }
        }
        else
        {
            _rb.AddForce(_rb.linearVelocity * (-_deceleration), ForceMode2D.Force);
        }
    }

    public void SetMovementActive(bool isActive)
    {
        _isMoving = isActive;
    }

    private Vector2 GetAnimationDirection()
    {
        Vector2 direction = Vector2.zero;

        if (Mathf.Abs(_moveDirection.x) >= Mathf.Abs(_moveDirection.y))
        {
            direction.x = Mathf.Sign(_moveDirection.x);
        }
        else
        {
            direction.y = Mathf.Sign(_moveDirection.y);
        }

        return direction;
    }
}