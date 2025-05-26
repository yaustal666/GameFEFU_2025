public class PlayerWalkState : State {

    private Player _player;

    public PlayerWalkState(Player player) {
        _player = player;
    }

    public override void Update() {
        _player._rb.linearVelocity = _player._moveDirection * _player._moveSpeed;
    }
}