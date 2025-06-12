using UnityEngine;

public class IdleState : State {

    private readonly Animator _animator;

    public IdleState() { }

    public override void Initialize(IStateData stateData) {
        _stateData = stateData;
    }

    public override void Enter() {
        _animator.SetTrigger("Idle");
    }


}