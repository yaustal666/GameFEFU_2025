using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/RoomsLibrary", fileName = "RoomsLibrary")]
public class PrefabCollection : ScriptableObject
{
    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] private string path;

    // add get some, that means everything must have id, that is ok, also need dictionary
    public GameObject GetRandom()
    {
        var roomIdx = Random.Range(0, prefabs.Count);
        return prefabs[roomIdx];
    }

    public GameObject Get(int idx)
    {
        return prefabs[idx];
    }

#if UNITY_EDITOR
    [ContextMenu("Load from Folder")]
    public void LoadFromFolder()
    {
        prefabs.Clear();

        GameObject[] loadedRooms = Resources.LoadAll<GameObject>("Rooms");
        prefabs.AddRange(loadedRooms);

        UnityEditor.EditorUtility.SetDirty(this);
        Debug.Log($"Loaded {prefabs.Count} rooms from Resources/Rooms");
    }
#endif
}