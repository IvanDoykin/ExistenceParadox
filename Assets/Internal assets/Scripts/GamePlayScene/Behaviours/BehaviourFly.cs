using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BehaviourFly", menuName = "Behaviours/BehaviourFly")]
public class BehaviourFly : CustomBehaviour, ITick, ICustomBehaviour
{
    public void Tick()
    {
        InstanceActor.transform.Translate(Vector3.up * Time.deltaTime);
    }

    public void ReceiveDataInstance(Actor actor)
    {
    }
}