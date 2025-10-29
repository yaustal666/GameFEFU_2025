using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    public Player _player;
    public CinemachineConfiner2D _confiner;

    private Dictionary<int, DungeonRoom> _mappingGridToRoom = new Dictionary<int, DungeonRoom>();
    [SerializeField] private PrefabCollection _roomsPrefabs;

    private int roomVerticalDistance = 100;
    private int roomHorizontalDistance = 100;

    private int dungeonSize = 7;
    private int dungeonFullSize;

    private int[] grid;
    private int startRoomCellIndex = 3;
    private int bossRoomCellIndex = 45;
    private int townRoomCellIndex = 27;
    private int generationStartCellIndex = 23;

    private bool isBossRoomConnected;
    private bool isStartRoomConnected;
    private bool isTownRoomConnected;

    public int roomCount;
    public int minRoomCount = 8;
    public int maxRoomCount = 15;

    public float roomSpawnChance;

    private int[] roomTraverseOrder;

    Queue<int> cellQueue;

    private void Start()
    {
        dungeonFullSize = dungeonSize * dungeonSize;
        roomTraverseOrder = new int[4];
        roomTraverseOrder[0] = -dungeonSize;
        roomTraverseOrder[1] = 1;
        roomTraverseOrder[2] = dungeonSize;
        roomTraverseOrder[3] = -1;

        GenerateDungeon();
    }

    private void GenerateDungeon()
    {
        grid = new int[dungeonSize * dungeonSize];
        grid[generationStartCellIndex] = 1;

        cellQueue = new Queue<int>();
        cellQueue.Enqueue(generationStartCellIndex);

        while (cellQueue.Count > 0)
        {
            int index = cellQueue.Dequeue();

            foreach (int direction in roomTraverseOrder)
            {
                VisitCell(index, direction);
            }
        }

        grid[startRoomCellIndex] = 1;
        grid[bossRoomCellIndex] = 1;
        grid[townRoomCellIndex] = 1;

        if (!isStartRoomConnected) { isStartRoomConnected = ConnectRoom(startRoomCellIndex, dungeonSize); }
        if (!isBossRoomConnected) { isBossRoomConnected = ConnectRoom(bossRoomCellIndex, -dungeonSize); }
        if (!isTownRoomConnected) { isTownRoomConnected = ConnectRoom(townRoomCellIndex, -1); }

        SpawnRooms();
    }

    private void RestartGeneration()
    {
        for (int i = 0; i < dungeonFullSize; i++)
        {
            grid[i] = 0;
        }
        roomCount = 0;
        GenerateDungeon();
    }

    private bool VisitCell(int index, int direction)
    {

        int value = GetGridValue(index, direction);
        if (value == -1)
        {
            return false;
        }

        if (index == startRoomCellIndex)
        {
            isStartRoomConnected = true;
            return true;
        }

        if (index == bossRoomCellIndex)
        {
            isBossRoomConnected = true;
            return true;
        }

        if (index == townRoomCellIndex)
        {
            isTownRoomConnected = true;
            return true;
        }

        if (value != 0 || roomCount == maxRoomCount)
        {
            return false;
        }

        if (Chance.Roll(0.8f))
        {
            if (GetNeighbourCount(index + direction) > 1)
            {
                return false;
            }
        }

        if (Chance.Roll(roomSpawnChance))
        {
            grid[index + direction] = 1;
            roomCount++;
            cellQueue.Enqueue(index + direction);
            return true;
        }

        return true;
    }

    private int GetNeighbourCount(int index)
    {
        int count = 0;
        int value;

        foreach (int direction in roomTraverseOrder)
        {
            value = GetGridValue(index, direction);
            if (value != -1)
            {
                count += value;
            }
        }

        return count;
    }

    private bool ConnectRoom(int index, int direction)
    {
        grid[index] = 1;
        int neighbourCount = GetNeighbourCount(index);
        if (neighbourCount > 1)
        {
            return true;
        }

        return ConnectRoom(index + direction, direction);
    }

    private void SpawnRooms()
    {
        for (int i = 0; i < dungeonSize; i++)
        {
            for (int j = 0; j < dungeonSize; j++)
            {
                int index = i * dungeonSize + j;
                if (grid[index] != 0)
                {
                    List<int> neighbours = new List<int>();

                    int value;
                    foreach (int direction in roomTraverseOrder)
                    {
                        value = GetGridValue(index, direction);
                        if (value == -1 || value == 0)
                        {
                            neighbours.Add(0);
                        }
                        else
                        {
                            neighbours.Add(index + direction);
                        }
                    }

                    var newRoom = Instantiate(_roomsPrefabs.GetRandom());
                    newRoom.transform.Translate(roomHorizontalDistance * (j - 3), -roomVerticalDistance * i, 0);

                    var dungeonRoom = newRoom.GetComponent<DungeonRoom>();
                    //dungeonRoom.InitializeDoors(index, neighbours);
                    dungeonRoom.PlayerExitRoom += OnPlayerExitRoom;

                    _mappingGridToRoom.Add(index, dungeonRoom);
                }
            }
        }
    }

    private void OnPlayerExitRoom(int cellId, int newCellid, int entryId)
    {
        var dungeonRoom = _mappingGridToRoom[newCellid];
        var entryPoint = dungeonRoom.GetEntryById(entryId);
        var cameraBoundary = dungeonRoom.GetCameraBoundaries();

        _player.Teleport(entryPoint);
        _confiner.BoundingShape2D = cameraBoundary;
    }

    public int GetGridValue(int index, int direction)
    {
        if (direction == 1)
        {
            if (index % dungeonSize < dungeonSize - 1)
            {
                return grid[index + direction];
            }
        }

        if (direction == -1)
        {
            if (index % dungeonSize > 0)
            {
                return grid[index + direction];
            }
        }

        if (direction == dungeonSize)
        {
            if (index <= dungeonFullSize - dungeonSize - 1)
            {
                return grid[index + direction];
            }
        }

        if (direction == -dungeonSize)
        {
            if (index >= dungeonSize)
            {
                return grid[index + direction];
            }
        }

        return -1;
    }

}