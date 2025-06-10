using UnityEngine;

public class BasicIdleState : State {

    private readonly IMoveStateData _stateData;
    private Vector2 _targetPosition;
    private float _reachedThreshold = 0.1f;

    public BasicIdleState(IMoveStateData stateData) {
        _stateData = stateData;
    }

    public override void Enter() {
        Debug.Log("Idle state");
        PickNewTargetPosition();
    }

    public override void Update() {
        Vector2 currentPosition = _stateData.Transform.position;
        _stateData.MoveDirection = (_targetPosition - currentPosition).normalized;

        _stateData.RB.linearVelocity = _stateData.MoveDirection * _stateData.Speed;

        if (Vector2.Distance(currentPosition, _targetPosition) < _reachedThreshold) {
            PickNewTargetPosition();
        }
    }

    private void PickNewTargetPosition() {
        Vector2 randomDirection = Random.insideUnitCircle * 2f;
        _targetPosition = (Vector2)_stateData.Transform.position + randomDirection;

    }

    public override void Exit() {
        _stateData.RB.linearVelocity = Vector2.zero;
        _stateData.MoveDirection = Vector2.zero;
    }

}
