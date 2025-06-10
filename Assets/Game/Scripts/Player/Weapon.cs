using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [SerializeField] private Animator _animator;
    [SerializeField] private int _currentAttack;

    [SerializeField] private bool isAttackExit = false;
    [SerializeField] private bool isIdle = true;
    public bool IsIdle => isIdle;

    [SerializeField] private List<AttackData> _combo;

    public void Awake() {
        _animator = GetComponent<Animator>();
        _currentAttack = 0;
    }

    public void Attack() {

        if (isAttackExit || isIdle) {
            if (_currentAttack == _combo.Count - 1 && !isAttackExit) {
                return;
            }

            isIdle = false;
            isAttackExit = false;
            _animator.SetTrigger(_combo[_currentAttack].attackName);
        } else {
            return;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var enemy = collision.gameObject.GetComponent<IDamagable>();
        if (enemy != null) {
            enemy.TakeDamage(_combo[_currentAttack].damage);
        }
    }

    public void ResetAttack() {
        Debug.Log("reset");
        isAttackExit = false;
        isIdle = true;
        _currentAttack = 0;
    }

    public void SetAttackExit() {

        if (_currentAttack == _combo.Count - 1) _currentAttack = 0;
        else {
            isAttackExit = true;
            _currentAttack++;
        }
    }

    public void StopAttack() {
        ResetAttack();
        _animator.SetTrigger("Reset");
    }
}