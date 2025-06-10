using UnityEngine;

public interface IMoveStateData {
    Rigidbody2D RB { get; }
    Vector2 MoveDirection { get; set; }
    float Speed { get; }

    Transform Transform { get; }

}