using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BehaviourFly", menuName = "Behaviours/BehaviourFly")]
public class BehaviourFly : Behaviour, ITick
{
    private void Awake()
    {
        ManagerUpdate.AddTo(this);
    }

    public void Tick()
    {
        Debug.Log(instanceActor.data.dataHp.health);
        instanceActor.transform.Translate(Vector3.up * Time.deltaTime);
    }
}