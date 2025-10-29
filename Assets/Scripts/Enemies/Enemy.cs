using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamagable
{
    public EnemyData _data;
    protected Rigidbody2D _rb;
    protected Health _health;
    protected Animator _anim;
    protected SpriteRenderer _sr;
    [SerializeField] protected Hurtbox hurtbox;

    public virtual void TakeDamage(Damage damage)
    {
        _health.GetDamage(damage.damage);
    }

    protected void Init()
    {
        _rb = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }
}