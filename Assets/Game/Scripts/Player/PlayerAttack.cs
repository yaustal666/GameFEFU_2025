using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour {
    private InputSystemActions _inputReader;
    private Player _player;

    private void Awake() {
        _inputReader = new InputSystemActions();
        _player = GetComponent<Player>();
    }

    private void OnEnable() {
        _inputReader.Player.Enable();
        _inputReader.Player.Attack.performed += OnAttack;
    }

    private void OnAttack(InputAction.CallbackContext context) {
        _player._weapon.Attack();
    }

    private void Update() {
        _player._isAttacking = !_player._weapon.IsIdle;
    }
}