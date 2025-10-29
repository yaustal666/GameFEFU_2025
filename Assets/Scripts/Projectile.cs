using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private float baseSpeed = 10f;
    [SerializeField] private float lifetime = 2f;

    public IObjectPool<Projectile> objectPool;
    public IObjectPool<Projectile> ObjectPool { set => objectPool = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectPool.Release(this);
    }

    public void Shoot(float speedMultiplier)
    {
        _rb.AddForce(-transform.right * baseSpeed * speedMultiplier, ForceMode2D.Impulse);
    }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(lifetime));
    }

    private IEnumerator DeactivateRoutine(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        _rb.linearVelocity = Vector2.zero;

        objectPool.Release(this);
    }
}