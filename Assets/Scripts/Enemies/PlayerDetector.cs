using System;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public event Action<Transform> PlayerDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Transform res;
            res = collision.gameObject.GetComponent<Transform>();
            PlayerDetected.Invoke(res);
        }
    }
}