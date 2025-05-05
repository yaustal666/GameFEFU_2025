using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private GameInput _gameInput;

    private Vector2 _moveDirection;
    private float _moveSpeed = 5f;

    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _hand;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _gameInput = new GameInput();
    }

    private void OnEnable() {
        _gameInput.Enable();
        _gameInput.Player.Attack.performed += OnPlayerAttack;
    }

    private void OnPlayerAttack(InputAction.CallbackContext context) {
        _weapon.Attack();
    }

    private void FixedUpdate() {
        _moveDirection = _gameInput.Player.Move.ReadValue<Vector2>();
        PlayerMove();
    }

    private void PlayerMove() {
        if(_moveDirection.x < -0.01) {
            _hand.localScale = new Vector3(-1, 1, 1);
        } else if (_moveDirection.x > 0.01) {
            _hand.localScale = new Vector3(1, 1, 1);
        }

        _rb.linearVelocity = _moveDirection * _moveSpeed;
    }

}
