using UnityEngine;

public class Health : MonoBehaviour, IHealth {
    private float _hp = 100f;

    public void TakeDamage(float damage) {
        _hp -= damage;
        if (_hp <= 0) {
            Destroy(gameObject);
        }
    }

    public void Heal(float heal) {
        _hp += heal;
    }
}
