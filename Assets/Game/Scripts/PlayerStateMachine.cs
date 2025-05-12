using UnityEngine;

public class PlayerStateMachine : MonoBehaviour {

    private StateMachine _fsm;

    private void Awake() {
        _fsm = new StateMachine();

    }
}