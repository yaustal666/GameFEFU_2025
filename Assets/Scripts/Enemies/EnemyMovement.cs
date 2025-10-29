using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Animator _anim;
    private NavMeshAgent _agent;

    public void MoveTo(Transform target)
    {
        _agent.SetDestination(target.position);
    }

    public void Stop()
    {

    }
}