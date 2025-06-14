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
            _stateData.Stop();
            return;
        }

        Vector2 moveDirection = (targetPosition - currentPosition).normalized;

        _stateData.Move(moveDirection);

    }

    public override void Exit() {
        _stateData.Stop();
    }

}