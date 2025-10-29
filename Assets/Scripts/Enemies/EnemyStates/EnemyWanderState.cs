using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public struct EnemyWanderStateConfig
{
    public float WaitTime;
    public float StopDistance;
}

public class EnemyWanderState : State
{
    private Enemy _enemy;
    private NavMeshAgent _agent;
    private float _waitTime;
    private float _stopDistance;
    private Vector2 _destination;

    public EnemyWanderState(EnemyWanderStateConfig config, Enemy enemy, NavMeshAgent agent)
    {
        _waitTime = config.WaitTime;
        _stopDistance = config.StopDistance;
        _enemy = enemy;
        _agent = agent;
    }

    public override void Enter()
    {
        _enemy.StartCoroutine(WanderRoutine());
    }

    public override void Exit() { 
        _enemy.StopCoroutine(WanderRoutine());
    }

    private void ChooseRandomDestination()
    {
        Vector2 randomDirection = UnityEngine.Random.insideUnitCircle;
        var dist = UnityEngine.Random.Range(5f, 8f);
        randomDirection *= dist;
        randomDirection += (Vector2)_enemy.transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, dist, -1);
        _destination = navHit.position;
    }

    private IEnumerator WanderRoutine()
    {
        while (true)
        {
            ChooseRandomDestination();
            _agent.SetDestination(_destination);
            _agent.isStopped = false;

            while (_agent.remainingDistance > _stopDistance)
            {
                yield return null;
            }

            _agent.isStopped = true;
            yield return new WaitForSeconds(_waitTime);
        }
    }
}