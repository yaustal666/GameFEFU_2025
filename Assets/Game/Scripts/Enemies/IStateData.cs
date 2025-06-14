using UnityEngine;

public interface IStateData {
    Transform Transform { get; }
    Transform Target { get; }
    Animator Animator { get; }

    void Move(Vector2 moveDirection);
    void Stop();
}