public class PlayerAttackState : State {

    private Player _player;

    public PlayerAttackState(Player player) {
        _player = player;
    }

    public override void Enter() {
        _player._weapon.Attack();
    }
}