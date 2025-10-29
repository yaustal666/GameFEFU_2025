using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct StateBehavior
{
    public MonoBehaviour component;
    public bool Active;
}

[Serializable]
public class NState
{
    [SerializeField] List<StateBehavior> _configuration;
}