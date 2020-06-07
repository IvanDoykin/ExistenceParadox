using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingCollider : MonoBehaviour, IEventTrigger
{
    private Entity _entity;
    private bool _isTriggering;

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
        _isTriggering = true;
        TriggerEvent($"{_entity.name}{DetectingEvents.EntityColliderTriggered}", otherCollider, _entity);
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        _isTriggering = false;
        StartCoroutine(CountdownAfterTriggerExit(otherCollider));
    }

    IEnumerator CountdownAfterTriggerExit(Collider otherCollider)
    {
        yield return new WaitForSeconds(6);
        if (_isTriggering == false)
        {
            TriggerEvent($"{_entity.name}{DetectingEvents.EntityColliderExit}", otherCollider, _entity);
        }
    }
}