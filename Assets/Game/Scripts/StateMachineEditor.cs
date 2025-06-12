using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;

[CustomEditor(typeof(StateMachine))]
public class StateMachineEditor : Editor {
    private Type[] _stateTypes;
    private string[] _stateTypeNames;

    private void OnEnable() {
        _stateTypes = Assembly.GetAssembly(typeof(State))
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(State)) && !t.IsAbstract)
            .ToArray();

        _stateTypeNames = _stateTypes.Select(t => t.Name).ToArray();
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        serializedObject.Update();

        SerializedProperty statesProp = serializedObject.FindProperty("_states");

        // Draw existing states with foldouts and remove buttons
        for (int i = 0; i < statesProp.arraySize; i++) {
            SerializedProperty stateProp = statesProp.GetArrayElementAtIndex(i);
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.BeginHorizontal();

            stateProp.isExpanded = EditorGUILayout.Foldout(stateProp.isExpanded, _stateTypes.FirstOrDefault(t => t.Name == stateProp.managedReferenceFullTypename.Split(' ')[1])?.Name ?? "State");

            if (GUILayout.Button("Remove")) {
                statesProp.DeleteArrayElementAtIndex(i);
                break; // Exit loop to avoid issues after removal
            }
            EditorGUILayout.EndHorizontal();

            if (stateProp.isExpanded) {
                EditorGUILayout.PropertyField(stateProp, true);
            }
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.Space();

        // Dropdown to add new state
        int selectedIndex = -1;
        selectedIndex = EditorGUILayout.Popup("Add State", selectedIndex, _stateTypeNames);
        if (selectedIndex >= 0) {
            // Add new instance of selected state type
            statesProp.arraySize++;
            SerializedProperty newStateProp = statesProp.GetArrayElementAtIndex(statesProp.arraySize - 1);
            newStateProp.managedReferenceValue = Activator.CreateInstance(_stateTypes[selectedIndex]);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
