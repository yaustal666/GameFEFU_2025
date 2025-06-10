using UnityEngine;

public interface IChaseStateData : IMoveStateData {
    Transform Target { get; }
}