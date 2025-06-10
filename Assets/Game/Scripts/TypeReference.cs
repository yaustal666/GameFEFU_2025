using System;
using UnityEngine;

[Serializable]
public class TypeReference : ISerializationCallbackReceiver {
    [SerializeField] private string assemblyQualifiedName;

    private Type type;

    public Type Type {
        get => type;
        set {
            type = value;
            assemblyQualifiedName = value?.AssemblyQualifiedName;
        }
    }

    public void OnBeforeSerialize() { }

    public void OnAfterDeserialize() {
        if (!string.IsNullOrEmpty(assemblyQualifiedName)) {
            type = Type.GetType(assemblyQualifiedName);
        } else {
            type = null;
        }
    }

    public static implicit operator Type(TypeReference typeReference) => typeReference?.Type;
    public static implicit operator TypeReference(Type type) => new TypeReference { Type = type };
}