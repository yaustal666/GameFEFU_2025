using Unity.Cinemachine;
using UnityEngine;


public class CameraWaypoint : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner2D confiner;
    [SerializeField] private Collider2D newBoundary;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        confiner.BoundingShape2D = newBoundary;
    }
}