using UnityEngine;

public class Player : MonoBehaviour, IMovableBody
{
    public Rigidbody2D _rb;

    public Vector2 _moveDirection;
    public float _moveSpeed = 5f;

    [SerializeField] public Weapon _weapon;
    [SerializeField] public Transform _hand;

    public bool _isAttacking = false;

    public Rigidbody2D Rigidbody => _rb;

    public float Speed { get => _moveSpeed; set => _moveSpeed = value; }
    public Vector2 MoveDirection { get => _moveDirection; set => _moveDirection = value; }

    public void RotateDirection(int side) {
        if(!_isAttacking) {
            _hand.localScale = new Vector3(side, 1, 1);
        }
    }

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }
}
