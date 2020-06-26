using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackCollider : MonoBehaviour, IEventTrigger
{
    private Entity _entity;
    private string _currentColliderName;

    private void Start()
    {
        _entity = GetComponentInParent<Entity>();
        _currentColliderName = name;
    }

    public void TriggerEvent(string eventName, params object[] arguments)
    {
        ManagerEvents.CheckTriggeringEvent(eventName, arguments);
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        TriggerEvent($"{_entity.name}{MeleeAttackEvents.MeleeAttackIsOnAvailableNow}", otherCollider, _entity,
            _currentColliderName);
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        TriggerEvent($"{_entity.name}{MeleeAttackEvents.MeleeAttackIsNotAvailableNow}", otherCollider, _entity,
            _currentColliderName);
    }
}