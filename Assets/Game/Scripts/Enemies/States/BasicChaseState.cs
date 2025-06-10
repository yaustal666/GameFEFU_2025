using UnityEngine;

public class BasicChaseState : State {

    private readonly IChaseStateData _stateData;

    public BasicChaseState(IChaseStateData stateData) {
        _stateData = stateData;
    }

    public override void Enter() {
        Debug.Log("Chase state");
    }

    public override void Update() {
        _stateData.MoveDirection = (_stateData.Target.position - _stateData.Transform.position).normalized;

        _stateData.RB.linearVelocity = _stateData.MoveDirection * _stateData.Speed;
    }

    public override void Exit() {
        _stateData.RB.linearVelocity = Vector2.zero;
        _stateData.MoveDirection = Vector2.zero;
    }
}