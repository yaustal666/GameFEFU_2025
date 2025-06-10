using UnityEngine;

public class ZombieStateMachine : MonoBehaviour {

    private Zombie _zombie;
    private StateMachine _fsm;

    [SerializeField]
    [TypeFilter(typeof(State))]
    TypeReference _idleState;


    [SerializeField]
    [TypeFilter(typeof(State))]
    TypeReference _chaseState;

    private void Start() {
        _zombie = GetComponent<Zombie>();

        _fsm = new StateMachine();
        _fsm.AddState(new BasicIdleState(_zombie));
        _fsm.AddState(new BasicChaseState(_zombie));

        _fsm.ChangeState<BasicIdleState>();
    }

    public void ToChaseState() {
        _fsm.ChangeState<BasicChaseState>();
    }

    private void Update() {
        _fsm.Update();
    }
}