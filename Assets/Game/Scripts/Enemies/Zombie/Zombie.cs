using System;
using UnityEngine;

public class Zombie : Enemy {

    [SerializeField] Detector _playerDetector;

    private void OnEnable() {
        _playerDetector.Detected += OnPlayerDetected;
    }

    private void OnPlayerDetected(Transform transform) {
        _target = transform;
        _stateMachine.ChangeState(StateType.Chase);
    }
}