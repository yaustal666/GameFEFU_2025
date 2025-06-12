using System;
using Unity.Behavior;
using UnityEngine;

public class Detector : MonoBehaviour {
    public event Action<Transform> Detected;


    private void OnTriggerEnter2D(Collider2D collision) {
        var target = collision.GetComponent<Player>();

        if (target != null) {
            Detected.Invoke(target.transform);
        }
    }
}