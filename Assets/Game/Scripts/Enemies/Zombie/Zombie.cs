using System;
using UnityEngine;

public class Zombie : Enemy {

    [SerializeField] Detector _playerDetector;
    [SerializeField] private Hitbox _hitbox;

    private void OnEnable() {
        _playerDetector.Detected += OnPlayerDetected;
        _hitbox.HitboxCollide += OnHitboxCollided;
    }

    private void OnHitboxCollided(IDamagable damagable) {
        Damage damage = new Damage();
        damage.DamageAmount = 10f;
        damage.DamageSourcePosition = transform;
        damagable.TakeDamage(damage);
    }

    private void OnPlayerDetected(Transform transform) {
        _target = transform;
        _stateMachine.ChangeState(StateType.Chase);
    }


    public override void Move(Vector2 moveDirection) { 
        _rb.linearVelocity = moveDirection * _speed;    
    }

    public override void Stop() {
        _rb.linearVelocity = Vector2.zero;
    }
}