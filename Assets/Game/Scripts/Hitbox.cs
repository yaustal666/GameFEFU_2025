using System;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public bool isAttack;
    private Collider2D _collider;
    private IDamagable _owner;
    public event Action<IDamagable> HitboxCollide;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Hitbox>(out var otherHitbox))
        {
            if (isAttack)
            {
                    HitboxCollide?.Invoke(otherHitbox._owner);
            }

        }

    }

    public void SetActive(bool isActive)
    {
        _collider.enabled = isActive;
    }
}