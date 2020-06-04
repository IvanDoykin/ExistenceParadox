using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingCollider : MonoBehaviour, IArgumentativeEventTrigger
{
    public void TriggerEvent(string eventName, params dynamic[] arguments)
    {
        ManagerEvents.TriggerEvent(eventName, arguments[0]);
    }

    private void OnTriggerEnter(Collider other)
    {
        TriggerEvent($"{transform.parent.name}{DetectingEvents.EntityColliderTriggered}", other);
    }
}