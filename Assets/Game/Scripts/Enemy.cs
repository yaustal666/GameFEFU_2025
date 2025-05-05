using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _hp = 100f;
    private float _lastDamageTakenTime = 0f;
    private float _takeDamageCooldown = 0.8f;

    public void GetDamage(float damage) {


        Debug.Log("Taken damage");
        _hp -= damage;
        if (_hp <= 0) {
            Destroy(gameObject);
        }
    }
}
