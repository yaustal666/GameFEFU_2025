using UnityEngine;

public interface IMovableBody {
    Rigidbody2D Rigidbody { get; }
    float Speed { get; set; }
    Vector2 MoveDirection { get; set; }

    void RotateDirection(int side);
}