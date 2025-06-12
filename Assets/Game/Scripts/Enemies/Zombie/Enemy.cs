using UnityEngine;

[RequireComponent (typeof(Rigidbody2D), typeof(Animator), typeof(StateMachine))]
public class Enemy : MonoBehaviour, IStateData {

    protected Rigidbody2D _rb;
    protected StateMachine _stateMachine;
    protected Transform _target;
    protected Animator _animator;
    protected Vector2 _moveDirection;
    [SerializeField] protected float _speed = 3f;

    public Rigidbody2D RB => _rb;
    public Transform Target => _target;
    public Transform Transform => transform;
    public Animator Animator => _animator;
    public Vector2 MoveDirection { get => _moveDirection; set => _moveDirection = value; }
    public float Speed => _speed;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _stateMachine = GetComponent<StateMachine>();
        _animator = GetComponent<Animator>();
    }

}