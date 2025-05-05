using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "SO/Attack")]
public class AttackData : ScriptableObject {
    public string attackName;
    public float damage;
}