using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BehaviourFly", menuName = "Behaviours/BehaviourFly")]
public class BehaviourFly : Behaviour, ITick, ICustomBehaviour
{
    private void Awake()
    {
        ManagerUpdate.AddTo(this);
    }

    public void Tick()
    {
        instanceActor.transform.Translate(Vector3.up * Time.deltaTime);
    }

    public void ReceiveData(Actor actor)
    {
        Debug.Log(instanceActor.GetInstanceID());
    }
}