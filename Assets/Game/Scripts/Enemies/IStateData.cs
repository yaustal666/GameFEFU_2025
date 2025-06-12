using UnityEngine;

public interface IStateData {
    Rigidbody2D RB { get; }
    Vector2 MoveDirection { get; set; }
    float Speed { get; }
    Transform Transform { get; }
    Transform Target { get; }
    Animator Animator { get; }
}