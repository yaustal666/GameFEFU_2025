using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public struct EnemyFollowStateConfig
{
    public float WaitTime;
    public float StopDistance;
}

public class EnemyFollowState : State
{
    private Func<Vector2> _target;
    private NavMeshAgent _agent;

    public EnemyFollowState(NavMeshAgent agent, Func<Vector2> target)
    {
        _agent = agent;
        _target = target;
    }

    public override void Update()
    {
        _agent.SetDestination(_target());
    }
}