using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private IAttacker _owner;
    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damagable = collision.GetComponent<Hurtbox>();
        if (damagable != null)
        {
            _owner.Attack(damagable.Owner);
        }
    }

    public void SetOwner(IAttacker owner)
    {
        Debug.Log("Owner Set");
        Debug.Log(owner);
        _owner = owner;
    }

    public void SetEnabled(bool enabled)
    {
        _collider.enabled = enabled;
    }
}