using System;

[Serializable]
public abstract class State {

    public StateType _type;
    protected IStateData _stateData;
    public abstract void Initialize(IStateData stateData);
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void PhysicsUpdate() { }
    public virtual void Exit() { }
}