using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private SpriteRenderer _sprite;

    private PlayerCombat _combat;
    private CharacterMovementTopDown _movement;

    [SerializeField] private Transform _hand;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _combat = GetComponent<PlayerCombat>();
        _movement = GetComponent<CharacterMovementTopDown>();
    }

    private void Update()
    {
        if (!_combat.IsAttacking)
        {
            _movement.SetMovementActive(true);

            if (!_movement.FacingLeft)
            {
                _sprite.flipX = false;
                _hand.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                _sprite.flipX = true;
                _hand.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else
        {
            _movement.SetMovementActive(false);
        }
    }

    public void Teleport(Transform position)
    {
        transform.position = position.position;
    }

    public void TakeDamage(Damage damage)
    {
        throw new System.NotImplementedException();
    }
}