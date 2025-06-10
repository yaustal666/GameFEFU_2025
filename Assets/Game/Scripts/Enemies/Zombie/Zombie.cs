using UnityEngine;

public class Zombie : MonoBehaviour, IChaseStateData {

    public Rigidbody2D _rb;
    public ZombieStateMachine _stateMachine;
    [SerializeField] private Detector _playerDetector;
    private Vector2 _moveDirection;
    private float _speed = 3f;
    private Transform _target;

    public Rigidbody2D RB => _rb;
    public Vector2 MoveDirection { get => _moveDirection; set => _moveDirection = value; }
    public float Speed => _speed;
    public Transform Target => _target;
    public Transform Transform => transform;

    private void OnEnable() {
        _playerDetector.Detected += OnPlayerDetected;
    }

    private void OnPlayerDetected(Transform playerPosition) {
        _target = playerPosition;
        _stateMachine.ToChaseState();
    }
}