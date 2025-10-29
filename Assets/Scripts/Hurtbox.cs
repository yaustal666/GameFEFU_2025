using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    private IDamagable _owner;
    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    public IDamagable Owner { get { return _owner; } }
    public void SetOwner(IDamagable owner)
    {
        _owner = owner;
    }
    public void SetEnabled(bool enabled)
    {
        _collider.enabled = enabled;
    }
}