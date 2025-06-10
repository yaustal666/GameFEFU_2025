using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Idle", story: "target wander around", category: "Action", id: "1040bbc18cfad3bbfc32bdf0ebb870f5")]
public partial class IdleAction : Action
{

    protected override Status OnStart()
    {

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

