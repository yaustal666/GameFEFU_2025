using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class Slime : Enemy
{
    [SerializeField] private NState _wanderSate = new NState();
    public PlayerDetector _playerDetector;
    public PlayerDetector _attackRange;
    public NavMeshAgent _agent;
    private Transform _target;

    public Material OnDamageEffectMaterial;
    private Material _baseMaterial;

    private Coroutine onDamageEffect;
    private Coroutine attack;

    [SerializeField] private EnemyWanderStateConfig _wanderStateConfig;
    private EnemyWanderState _wanderState;
    private EnemyFollowState _followState;
    private StateMachine _fsm;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        _wanderState = new EnemyWanderState(_wanderStateConfig, this, _agent);
        _followState = new EnemyFollowState(_agent, GetTargetPostion);

        _fsm = this.AddComponent<StateMachine>();
        _fsm.ChangeState(_wanderState);

        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        hurtbox.SetOwner(this);

        _baseMaterial = _sr.material;

        _health.OutOfHealth += OnEndHealth;
        _attackRange.PlayerDetected += OnInAttackRange;
        _playerDetector.PlayerDetected += OnPlayerDetected;
    }

    private void OnInAttackRange(Transform transform)
    {
        if (attack != null)
        {
            StopCoroutine(attack);
        }

        attack = StartCoroutine(Attack());
    }

    private void Update()
    {
        if (_agent.velocity.magnitude != 0)
        {
            _anim.SetFloat("x", _agent.velocity.x);
            _anim.SetFloat("y", _agent.velocity.y);
        }

        _anim.SetFloat("magnitude", _agent.velocity.magnitude);

        if (_agent.velocity.x > 0)
        {
            _sr.flipX = false;
        }
        else if (_agent.velocity.x < 0)
        {
            _sr.flipX = true;
        }
    }

    private void OnEndHealth()
    {
        Destroy(gameObject);
    }

    public Vector2 GetTargetPostion()
    {
        return _target.transform.position;
    }

    private void OnPlayerDetected(Transform transform)
    {
        _target = transform;
        _fsm.ChangeState(_followState);
    }

    public override void TakeDamage(Damage damage)
    {
        _health.GetDamage(damage.damage);
        var damageDirection = (transform.position - damage.damagePos.position).normalized;
        OnDamageEffect();
        Knockback(damageDirection);
    }

    private void Knockback(Vector2 direction)
    {
// need to stop coroutines or do smth else
        _agent.isStopped = true;
        _rb.AddForce(direction * 10, ForceMode2D.Impulse);
        _agent.isStopped = false;
    }

    private void OnDamageEffect()
    {
        if (onDamageEffect != null)
        {
            StopCoroutine(onDamageEffect);
        }

        onDamageEffect = StartCoroutine(DamageEffect());
    }

    private IEnumerator DamageEffect()
    {
        _sr.material = OnDamageEffectMaterial;
        yield return new WaitForSeconds(0.2f);
        _sr.material = _baseMaterial;
    }

    private IEnumerator Attack()
    {
        _agent.isStopped = true;
        yield return new WaitForSeconds(1f);
        _anim.SetTrigger("attack");
        yield return new WaitForSeconds(0.5f);
        _agent.isStopped = false;
    }
}