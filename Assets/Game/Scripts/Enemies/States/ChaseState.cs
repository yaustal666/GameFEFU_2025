using System;
using UnityEngine;

[Serializable]
public class ChaseState : State {

    [SerializeField] private float _reachedThreshold = 0.1f;

    public ChaseState() {}

    public override void Initialize(IStateData stateData) {
        _stateData = stateData;
    }

    public override void Enter() {
        Debug.Log("Chase state");
    }

    public override void Update() {
        Vector2 currentPosition = _stateData.Transform.position;
        Vector2 targetPosition = _stateData.Target.position;

        if (Vector2.Distance(currentPosition, targetPosition) < _reachedThreshold) {
            Stop();
            return;
        }

        _stateData.MoveDirection = (targetPosition - currentPosition).normalized;

        _stateData.RB.linearVelocity = _stateData.MoveDirection * _stateData.Speed;


    }

    public override void Exit() {
        _stateData.RB.linearVelocity = Vector2.zero;
        _stateData.MoveDirection = Vector2.zero;
    }

    private void Stop() {
        _stateData.RB.linearVelocity = Vector2.zero;
        _stateData.MoveDirection = Vector2.zero;
    }

}