using System;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public Type targetType;
    public Type dataAsked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetType.IsInstanceOfType(collision.gameObject))
        {
            
        }
        collision.gameObject.GetComponent(targetType);
    }
}
