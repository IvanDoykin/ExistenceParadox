using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingCollider : MonoBehaviour, IArgumentativeEventTrigger
{
    public void TriggerEvent(string eventName, params dynamic[] arguments)
    {
        ManagerEvents.CheckTriggeringEvent(eventName, arguments);
    }


    private void OnTriggerEnter(Collider other)
    {
        string parentName = transform.parent.name;
        TriggerEvent($"{parentName}{DetectingEvents.EntityColliderTriggered}", other, parentName);
    }
}