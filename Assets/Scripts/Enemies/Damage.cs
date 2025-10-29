using UnityEngine;

public class Damage {
    public Transform damagePos;
    public float damage;

    public Damage(Transform damagePos, float damage)
    {
        this.damagePos = damagePos;
        this.damage = damage;
    }
}