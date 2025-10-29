using System;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    public event Action<int, int, int> PlayerExitRoom;
    [SerializeField] private List<DungeonRoomDoor> _doors;
    [SerializeField] private Collider2D cameraBoundary;

    private void Start()
    {
        _doors = new List<DungeonRoomDoor>();
        foreach (var door in _doors) {
            door.PlayerLeaveRoom += OnPlayerLeaveRoom;
        }    
    }

    private void OnPlayerLeaveRoom(int cellId, int newCellid, int entryId)
    {
        PlayerExitRoom.Invoke(cellId, newCellid, entryId);
    }

    public Transform GetEntryById(int entryId)
    {
        return _doors[entryId].getEntryPoint();
    }

    public Collider2D GetCameraBoundaries()
    {
        return cameraBoundary;
    }

    public void InitializeDoors(int cellId, List<int> correspondingDoors)
    {
        for (int i = 0; i < 4; i ++)
        {
            int exit = correspondingDoors[i];
            if (exit == 0)
            {
                _doors[i].CloseDoor();
                _doors[i].gameObject.SetActive(false);
                continue;
            }

            _doors[i].Initialize(cellId, i);
            _doors[i].SetExit(correspondingDoors[i]);
        }
    }
}