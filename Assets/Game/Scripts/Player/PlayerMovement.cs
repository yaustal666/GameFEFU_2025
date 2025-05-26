using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    private InputSystemActions _inputReader;
    private IMovableBody _player;

    private void Awake() {
        _inputReader = new InputSystemActions();
        _player = GetComponent<IMovableBody>();
    }

    private void OnEnable() {
        _inputReader.Player.Enable();
    }

    private void Update() {
        _player.MoveDirection = _inputReader.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate() {

        if (_player.MoveDirection.x < -0.01) {
            _player.RotateDirection(-1);
        } else if (_player.MoveDirection.x > 0.01) {
            _player.RotateDirection(1);
        }

        _player.Rigidbody.linearVelocity = _player.MoveDirection * _player.Speed;
    }
}