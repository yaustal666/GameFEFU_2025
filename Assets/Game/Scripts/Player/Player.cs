using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    public Rigidbody2D _rb;
    public Health _hp;
    public float _knockbackPower;
    public float _knockbackDuration;
    public float _movementDisableTime;
    bool isKnockback;
    float knockbackTimer;

    public float _acceleration;
    public float _deceleration;
    public float _maxSpeed;

    public Vector2 _moveDirection;
    public float _moveSpeed = 5f;

    [SerializeField] public Weapon _weapon;
    [SerializeField] public Transform _hand;
    [SerializeField] private Hitbox _hitbox;

    public bool _isAttacking = false;
    private InputSystemActions _inputReader;

    public void RotateDirection(int side) {
        if(!_isAttacking) {
            _hand.localScale = new Vector3(side, 1, 1);
        }
    }

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _inputReader = new InputSystemActions();
        _inputReader.Player.Enable();
        _hitbox.HitboxCollide += OnHitboxCollided;
    }

    private void Update() {
        _moveDirection = _inputReader.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate() {

        if (isKnockback) {
            knockbackTimer -= Time.fixedDeltaTime;
            if (knockbackTimer <= 0f) {
                isKnockback = false;
            }
        } else {
            RotateHand();
            Move();
        }

    }

    private void OnHitboxCollided(IDamagable damagable) {
        Damage damage = new Damage();
        damage.DamageAmount = 10f;
        damage.DamageSourcePosition = transform;
        damagable.TakeDamage(damage);
    }

    public void TakeDamage(Damage damage) {
        Vector2 damageDirection = (transform.position - damage.DamageSourcePosition.position).normalized * _knockbackPower;
        _rb.AddForce(damageDirection * _knockbackPower, ForceMode2D.Impulse);
        _hp.TakeDamage(damage.DamageAmount);
    }

    private void Move() {
        if (_moveDirection != Vector2.zero) { 
            _rb.AddForce(_moveDirection * _acceleration, ForceMode2D.Force);

            if (_rb.linearVelocity.magnitude > _maxSpeed) {
                _rb.linearVelocity = _rb.linearVelocity.normalized * _maxSpeed;
            }
        } else {
            _rb.AddForce(_rb.linearVelocity * (-_deceleration), ForceMode2D.Force);
        }
    }

    private void RotateHand() {
        if (_moveDirection.x < -0.01) {
            RotateDirection(-1);
        } else if (_moveDirection.x > 0.01) {
            RotateDirection(1);
        }
    }
}
