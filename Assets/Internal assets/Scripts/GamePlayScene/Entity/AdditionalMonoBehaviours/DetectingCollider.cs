using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingCollider : MonoBehaviour, IEventTrigger
{
    private Entity _entity;

    private void Start()
    {
        _entity = GetComponentInParent<Entity>();
    }

    public void TriggerEvent(string eventName, params dynamic[] arguments)
    {
        ManagerEvents.CheckTriggeringEvent(eventName, arguments);
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        TriggerEvent($"{_entity.name}{DetectingEvents.EntityColliderTriggered}", otherCollider, _entity);
    }
}