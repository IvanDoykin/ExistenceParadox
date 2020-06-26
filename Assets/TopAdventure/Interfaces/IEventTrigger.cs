using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = System.Object;

public interface IEventTrigger
{
    void TriggerEvent(string eventName, params Object[] arguments);
}