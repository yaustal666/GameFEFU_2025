public abstract class State {
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void PhysicsUpdate() { }
    public virtual void Exit() { }
}