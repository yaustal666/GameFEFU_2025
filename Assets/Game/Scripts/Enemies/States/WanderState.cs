using System;
using UnityEngine;

[Serializable]
public class WanderState : State {

    [SerializeField] private float _reachedThreshold = 0.1f;
    [SerializeField] private float _wanderRadius = 0.1f;
    [SerializeField] private float _waitTime = 1f;
    private float _waitStartTime;
    private Vector2 _targetPosition;
    private bool isWaiting;

    public WanderState() {}

    public override void Initialize(IStateData stateData) {
        _stateData = stateData;
    }

    public override void Enter() {
        _stateData.Animator.SetTrigger("Move");
        PickNewTargetPosition();
    }
    
    public override void PhysicsUpdate() {
        if (Time.time - _waitStartTime > _waitTime) {
            isWaiting = false;
        }

        if (isWaiting) {
            return;
        }

        Vector2 currentPosition = _stateData.Transform.position;
        var moveDirection = (_targetPosition - currentPosition).normalized;

        _stateData.Move(moveDirection);

        if (Vector2.Distance(currentPosition, _targetPosition) < _reachedThreshold) {
            Stop();
            _waitStartTime = Time.time;
            isWaiting = true;
            PickNewTargetPosition();
        }
    }

    private void PickNewTargetPosition() {
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle * _wanderRadius;
        _targetPosition = (Vector2)_stateData.Transform.position + randomDirection;

    }

    public override void Exit() {
        Stop();
    }

    private void Stop() {
        _stateData.Animator.SetTrigger("idle");
        _stateData.Stop();
    }
}
