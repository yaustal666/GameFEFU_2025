#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

[CustomPropertyDrawer(typeof(TypeReference))]
public class TypeReferenceDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        // Get the assemblyQualifiedName property
        var typeNameProperty = property.FindPropertyRelative("assemblyQualifiedName");
        var currentTypeName = typeNameProperty.stringValue;
        Type currentType = null;

        if (!string.IsNullOrEmpty(currentTypeName)) {
            currentType = Type.GetType(currentTypeName);
        }

        // Get all types that inherit from a base type (if specified)
        TypeFilterAttribute typeFilter = fieldInfo.GetCustomAttributes(typeof(TypeFilterAttribute), true)
            .FirstOrDefault() as TypeFilterAttribute;

        Type[] availableTypes;
        if (typeFilter != null && typeFilter.BaseType != null) {
            availableTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsSubclassOf(typeFilter.BaseType) && !t.IsAbstract)
                .ToArray();
        } else {
            // If no filter, show all types (this might be too many!)
            availableTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract)
                .ToArray();
        }

        // Create display names
        string[] typeNames = availableTypes.Select(t => t.FullName).ToArray();
        int currentIndex = Array.IndexOf(availableTypes, currentType);

        // Draw the dropdown
        position = EditorGUI.PrefixLabel(position, label);
        int newIndex = EditorGUI.Popup(position, currentIndex, typeNames);

        if (newIndex >= 0 && newIndex < availableTypes.Length && newIndex != currentIndex) {
            typeNameProperty.stringValue = availableTypes[newIndex].AssemblyQualifiedName;
        }

        EditorGUI.EndProperty();
    }
}

// Optional attribute to filter types
[AttributeUsage(AttributeTargets.Field)]
public class TypeFilterAttribute : Attribute {
    public Type BaseType { get; }

    public TypeFilterAttribute(Type baseType) {
        BaseType = baseType;
    }
}
#endif