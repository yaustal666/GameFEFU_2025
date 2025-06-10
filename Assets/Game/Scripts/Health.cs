using UnityEngine;

public class Health : MonoBehaviour, IDamagable {
    private float _hp = 100f;

    public void TakeDamage(float damage) {
        Debug.Log("Taken damage");
        _hp -= damage;
        if (_hp <= 0) {
            Destroy(gameObject);
        }
    }
}
