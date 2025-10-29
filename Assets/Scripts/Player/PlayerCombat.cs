using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour, IAttacker
{
    private Animator _anim;
    private InputAction _attackAction;

    [SerializeField] private Weapon _weapon;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private Transform hand;

    public bool IsAttacking { get; private set; }

    private void Start()
    {
        _anim = GetComponent<Animator>();

        _attackAction = InputSystem.actions.FindAction("Attack");
        _attackAction.performed += OnAttack;

        EquipWeapon();
    }

    private void Update()
    {
        IsAttacking = _weapon.IsAttacking;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        _weapon.Attack();
    }

    public void EquipWeapon()
    {
        Debug.Log("Equip Weapon");
        var newWeapon = Instantiate(weaponPrefab);
        newWeapon.transform.SetParent(hand.transform, false);
        newWeapon.name = "weapon";

        _weapon = newWeapon.GetComponent<Weapon>();
        _weapon.Initialize(_anim, this);

        _anim.Rebind();
    }

    public void Attack(IDamagable damagable)
    {
        Debug.Log("ATTAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAK");
        damagable.TakeDamage(new Damage(transform, 10f));
    }
}