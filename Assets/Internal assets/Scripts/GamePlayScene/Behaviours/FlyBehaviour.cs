using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fly", menuName = "CustomBehaviours/FlyBehaviour")]
public class FlyBehaviour : CustomBehaviour, ITick
{
    protected override void InitializeCurrentBehaviourByReceivedEntityInstance(Entity instance)
    {
    }

    public void Tick()
    {
    }

    public override void TriggerEvent(string eventName, params dynamic[] arguments)
    {
    }

    public override void Subscribe()
    {
    }

    public override void UnSubscribe()
    {
    }
}