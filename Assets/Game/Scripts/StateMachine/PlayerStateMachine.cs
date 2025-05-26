using System;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour {

    private StateMachine _fsm;
    private Player _player;

    private void Awake() {
        _player = GetComponent<Player>();
        _fsm = new StateMachine();
        _fsm.AddState(new PlayerWalkState(_player));
        _fsm.AddState(new PlayerAttackState(_player));

        _fsm.ChangeState<PlayerWalkState>();

    }

    private void SwitchToAttack() {
        if (_fsm.CurrentState.GetType() == typeof(PlayerAttackState)) { 
            _fsm.CurrentState.Enter();
        } else {
            _fsm.ChangeState<PlayerAttackState>();
        }
    }

    private void SwitchToWalk() {
        _fsm.ChangeState<PlayerWalkState>();
    }

    private void Update() {
        _fsm.Update();
    }

    private void FixedUpdate() {
        _fsm.PhysicsUpdate();
    }
}