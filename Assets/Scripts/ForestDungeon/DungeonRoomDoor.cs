using System;
using UnityEngine;

public class DungeonRoomDoor : MonoBehaviour
{
    public event Action<int, int, int> PlayerLeaveRoom;

    public DungeonRoomDoorId id;
    public DungeonRoomDoorId correspondingDoorId;
    public GameObject closedDoor;
    [SerializeField] private Transform entryPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerLeaveRoom.Invoke(id.CellId, correspondingDoorId.CellId, correspondingDoorId.Id);
        }
    }

    public void Initialize(int cellId, int id)
    {
        this.id.Id = id;
        this.id.CellId = cellId;
    }

    public void SetExit(int exitId)
    {
        correspondingDoorId.CellId = exitId;
        correspondingDoorId.Id = (id.Id + 2) % 4;
    }

    public Transform getEntryPoint()
    {
        return entryPoint;
    }

    public void CloseDoor()
    {
        closedDoor.SetActive(true);
    }

    public void OpenDoor()
    {
        closedDoor.SetActive(false);
    }
}