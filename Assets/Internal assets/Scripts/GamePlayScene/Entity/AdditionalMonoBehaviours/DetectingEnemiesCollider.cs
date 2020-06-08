using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectingEnemiesCollider : MonoBehaviour, IEventTrigger
{
    private Entity _entity;
    private bool _isTriggering;
    private string _currentColliderName;

    private void Start()
    {
        _entity = GetComponentInParent<Entity>();
        _currentColliderName = name;
    }

    public void TriggerEvent(string eventName, params dynamic[] arguments)
    {
        ManagerEvents.CheckTriggeringEvent(eventName, arguments);
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        _isTriggering = true;
        TriggerEvent($"{_entity.name}{DetectingEvents.EntityDetectingColliderTriggered}", otherCollider, _entity,
            _currentColliderName);
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
            TriggerEvent($"{_entity.name}{DetectingEvents.EntityDetectingColliderExit}", otherCollider, _entity,
                _currentColliderName);
        }
    }
}