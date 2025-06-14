using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    [SerializeReference] private List<State> _states;

    private IStateData _stateData;
    private Dictionary<StateType, State> _statesMap = new Dictionary<StateType, State>();

    public State CurrentState { get; private set; }

    private void Start() {
        _stateData = GetComponent<IStateData>();

        foreach (var state in _states) {
            state.Initialize(_stateData);
            AddState(state);
        }
    }

    public void AddState(State state) {
        _statesMap.Add(state._type, state);
    }

    public void ChangeState(StateType type) {
        if (CurrentState != null && CurrentState._type == type) {
            return;
        }

        if (_statesMap.TryGetValue(type, out var newState)) {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }

    public void Update() {
        CurrentState?.Update();
    }

    public void FixedUpdate() {
        CurrentState?.PhysicsUpdate();
    }
}