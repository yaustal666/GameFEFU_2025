using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Wander", story: "[Entity] wander around in defferent directions inside a [zone]", category: "Action", id: "758719a6682eaa740b09b64d6d549543")]
public partial class WanderAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Entity;
    [SerializeReference] public BlackboardVariable<float> Zone;

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

