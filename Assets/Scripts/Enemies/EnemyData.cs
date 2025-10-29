using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private float maxHp;
    [SerializeField] private RandomValueFloat wanderStopTime;
    [SerializeField] private float wanderMaxDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Animator anim;

    public float MaxHp => maxHp;
    public float WanderMaxDistance => wanderMaxDistance;
    public RandomValueFloat WanderStopTime => wanderStopTime;
    public float MoveSpeed => moveSpeed;
    public Animator Animator => anim;
}